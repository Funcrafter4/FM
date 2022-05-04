using Findmaster.DataAccessLayer.Entity;
using Findmaster.DataAccessLayer.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Findmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VacanciesController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet("Get All Vacancies")]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = _context.Vacancies.ToListAsync();
            return Ok(vacancies);
        }

        [HttpPost("Add Vacancy")]
        public async Task<IActionResult> AddVacancies(Vacancy vacancy)
        {
            _context.Vacancies.Add(vacancy);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("{VacancyId}")]
        public async Task<IActionResult> GetVacancy(int VacancyId)
        {
            var dbVacancy = await _context.Vacancies.FirstOrDefaultAsync(v => v.VacancyId == VacancyId);
            if (dbVacancy == null)
            {
                return NotFound("Vacancy with given Id doesn't exist");
            }

            return Ok(dbVacancy);
        }

        [HttpPut("{VacancyId}")]
        public async Task<IActionResult> EditVacancy(int VacancyId, Vacancy vacancy)
        {
            var dbVacancy = await _context.Vacancies.FirstOrDefaultAsync(v => v.VacancyId == VacancyId);
            if (dbVacancy == null)
            {
                return NotFound("Vacancy with given Id doesn't exist");
            }

            dbVacancy = vacancy;

            await _context.SaveChangesAsync();

            return Ok(dbVacancy);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(String name)
        {

            var vacancies = _context.Vacancies.Where(v => EF.Functions.Like(v.VacancyName, $"%{name}%"));

            return Ok(vacancies);
        }

        [HttpPost("Add Application")]
        public async Task<IActionResult> AddApplication(Applications applications)
        {
            _context.Applications.Add(applications);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Add Favourite")]
        public async Task<IActionResult> AddFavourite(Favourite favourite)
        {
            
            _context.Favourites.Add(favourite);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Get All Favourites")]
        public async Task<IActionResult> GetFavourites(int UserId)
        {

            var favourites = _context.Favourites.Where(u => u.UserId == UserId);
            //favourites.UserId figure out how make(1 is placeholder)
            var dbvacancy = _context.Vacancies.Where(v => v.VacancyId == 1);
            return Ok(dbvacancy);
        }

        [HttpGet("Get All Applications")]
        public async Task<IActionResult> GetApplication(int UserId)
        {

            var applications = _context.Applications.Where(u => u.UserId == UserId);
            //applications.UserId figure out how make(1 is placeholder)
            var dbvacancy = _context.Vacancies.Where(v => v.VacancyId == 1);
            return Ok(dbvacancy);
        }
    }
}
