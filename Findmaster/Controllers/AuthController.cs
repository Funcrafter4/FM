using Findmaster.DataAccessLayer.DataContext;
using Findmaster.DataAccessLayer.DTO;
using Findmaster.DataAccessLayer.Entity;
using Findmaster.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Findmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        public int VerificationCode;

        public AuthController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(String UserEmail, String UserPassword, String UserName, String UserSurname)
        {
            var dbuser = await _context.Users.Where(u => u.UserEmail == UserEmail).FirstOrDefaultAsync();
            if (dbuser == null)
            {
                CreatePasswordHash(UserPassword, out byte[] UserPasswordHash, out byte[] UserPasswordSalt);
                _context.Users.Add(new User(UserEmail, UserPasswordHash, UserPasswordSalt));
                //_context.Users_Info.Add(user_Info);
                await _context.SaveChangesAsync();
                return Ok(dbuser);
            }
            return BadRequest("User Exists");
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(String UserEmail, String UserPassword)
        {
            var dbuser = await _context.Users.Where(u => u.UserEmail == UserEmail).FirstOrDefaultAsync();
            
            if (dbuser == null)
            {
                return BadRequest("User not found");
            }

            if(!VerifyPasswordHash(UserPassword, dbuser.UserPasswordHash, dbuser.UserPasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(dbuser);

            return Ok(token);

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserEmail)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        private void CreatePasswordHash(string UserPassword, out byte[] UserPasswordHash, out byte[] UserPasswordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                UserPasswordSalt = hmac.Key;
                UserPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(UserPassword));
            }
        }
        private bool VerifyPasswordHash(string UserPassword, byte[] UserPasswordHash, byte[] UserPasswordSalt)
        {
            using (var hmac = new HMACSHA512(UserPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(UserPassword));
                return computedHash.SequenceEqual(UserPasswordHash);
            }
        }

        [HttpPost("SendVerification")]
        public async Task<IActionResult> SendVerificationMail(string mail)
        {
            Random random = new Random();
            VerificationCode = random.Next(1000, 9999);
            String VerificationCodeString = VerificationCode.ToString();
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(mail, "Verification Code", VerificationCodeString);
            return Ok();
        }

        [HttpPost("CompareVerification")]
        public async Task<IActionResult> CompareVerificationCode(string code)
        {
            int codenumber = int.Parse(code);
            Console.WriteLine(codenumber + " " + VerificationCode);
            if(codenumber == VerificationCode)
            {
                return Ok();    
            }
            return BadRequest();
        }
    }
}
