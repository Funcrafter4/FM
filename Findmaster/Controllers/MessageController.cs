using Findmaster.DataAccessLayer.DataContext;
using Findmaster.DataAccessLayer.Entity;
using Findmaster.DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Findmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public MessageController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("Get_Chat")]

        public async Task<IActionResult> Get_Chat(int FromUserId, int ToUserId, int VacancyId)
        {
            var dbmessages = _context.Messages.Where(m => (((m.FromUserId == FromUserId && m.ToUserId == ToUserId) || (m.FromUserId == ToUserId && m.ToUserId == FromUserId))) && m.VacancyId == VacancyId);
            return Ok(dbmessages);
        }

        [HttpGet("Get_Chat_List_Employee")]
        public async Task<IActionResult> Get_Chat_List_Employee(int FromUserId)
        {
           
            var dbmessages = _context.Messages.Where(m => m.FromUserId == FromUserId).GroupBy(m => new { m.ToUserId, m.FromUserId })
                .Select(m => m.First()).ToList().Join(_context.Users_Info, m => m.ToUserId, u => u.UserId, (m, u) => new {
                    UserId = u.UserId,
                    Name = u.UserName,
                    Surname = u.UserSurname,
                    Message = m.Message
                });
            return Ok(dbmessages);
        }

        [HttpGet("Get_Chat_List_Employer")]
        public async Task<IActionResult> Get_Chat_List_Employer(int ToUserId)
        {
            var dbmessages = _context.Messages.Where(m => m.ToUserId == ToUserId).GroupBy(m => new { m.ToUserId, m.FromUserId })
                .Select(m => m.First()).ToList().Join(_context.Users_Info, m => m.FromUserId, u => u.UserId, (m, u) => new { 
                    UserId = u.UserId,
                    Name = u.UserName,
                    Surname = u.UserSurname,
                    Message = m.Message
                });
            
            
            return Ok(dbmessages);
        }


        [HttpPost("Send_Message")]
        public async Task<IActionResult> Send_Message(Messages messages)
        {
            _context.Messages.Add(messages);
            _context.SaveChanges();
            return Ok(messages);
        }
    }
}
