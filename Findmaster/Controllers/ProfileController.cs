using Findmaster.DataAccessLayer.Entity;
using Findmaster.DataAccessLayer.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findmaster.Controllers
{
    [Route("api/controller/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public ProfileController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUser(int UserId)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);

            return Ok(dbUser);
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> EditUser(int UserId, User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);


            dbUser = user;

            await _context.SaveChangesAsync();

            return Ok(dbUser);
        }

    }
}
