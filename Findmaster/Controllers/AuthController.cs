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
    [Route("api/[controller]/auth")]
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
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var dbuser = await _context.Users.FirstOrDefaultAsync();
            if (dbuser == null)
            {
                CreatePasswordHash(request.UserPassword, out byte[] UserPasswordHash, out byte[] UserPasswordSalt);
                dbuser.UserEmail = request.UserEmail;
                dbuser.UserPasswordHash = UserPasswordHash;
                dbuser.UserPasswordSalt = UserPasswordSalt;

                return Ok(dbuser);
            }
            return BadRequest("User Exists");
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var dbuser = await _context.Users.FirstOrDefaultAsync();

            if (dbuser.UserEmail != request.UserEmail)
            {
                return BadRequest("User not found");
            }

            if(!VerifyPasswordHash(request.UserPassword, dbuser.UserPasswordHash, dbuser.UserPasswordSalt))
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

        private async void SendVerificationMail(string mail)
        {
            Random random = new Random();
            VerificationCode = random.Next(1000, 9999);
            String VerificationCodeString = VerificationCode.ToString();
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(mail, "Verification Code", VerificationCodeString);
        }

        //placeholder need to think about logic(when connect front to back)
        private bool CompareVerificationCode(string code)
        {
            int codenumber = int.Parse(code);
            if(codenumber == VerificationCode)
            {
                return true;    
            }
            return false;
        }
    }
}
