﻿using Findmaster.DataAccessLayer.Entity;
using Findmaster.DataAccessLayer.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findmaster.Controllers
{
    [Route("api/[controller]")]
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
            var dbUser = await _context.Users_Info.FirstOrDefaultAsync(u => u.UserId == UserId);

            return Ok(dbUser);
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> EditUser(int UserId, User_Info user_Info)
        {
            var dbUser = await _context.Users_Info.FirstOrDefaultAsync(u => u.UserId == UserId);

            
            dbUser = user_Info;

            await _context.SaveChangesAsync();

            return Ok(dbUser);
        }

        [HttpGet("Is_Employeer")]
        public async Task<ActionResult<bool>> IsEmployeer(int UserId)
        {
            var dbUser = await _context.Users_Type.FirstOrDefaultAsync(u => u.UserId == UserId);
            if(dbUser == null)
            {
                return BadRequest("User not found");
            }
            if (dbUser.UserType == true)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
