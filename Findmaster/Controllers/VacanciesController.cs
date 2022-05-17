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


        [HttpGet("Get_All_Vacancies")]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = _context.Vacancies;
            return Ok(vacancies);
        }

        [HttpPost("Add_Vacancy")]
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

        [HttpPost("Add_Application")]
        public async Task<IActionResult> AddApplication(int UserId, int VacancyId)
        {
            _context.Applications.Add(new Applications(UserId, VacancyId));
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Add_Favourite")]
        public async Task<IActionResult> AddFavourite(int UserId, int VacancyId)
        {
            
            _context.Favourites.Add(new Favourite(UserId,VacancyId));
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Get_All_Favourites")]
        public async Task<IActionResult> GetFavourites(int UserId)
        {

            var dbvacancy = (from u in _context.Favourites
                             join v in _context.Vacancies on u.VacancyId equals v.VacancyId
                             where u.UserId == UserId
                             select new
                             {
                                 v.VacancyId,
                                 v.VacancyName,
                                 v.VacancyAddress,
                                 v.VacancyExp,
                                 v.VacancySalary,
                                 v.VacancyDescription,
                                 v.VacancyEmployerName,
                                 v.VacancyEmploymentType,
                                 v.VacancyRequirements
                             }).ToList();

            return Ok(dbvacancy);
        }

        [HttpGet("Get_All_Applications")]
        public async Task<IActionResult> GetApplication(int UserId)
        {

            var dbvacancy = (from u in _context.Applications
                             join v in _context.Vacancies on u.VacancyId equals v.VacancyId
                             where u.UserId == UserId
                             select new
                             {
                                 v.VacancyId,
                                 v.VacancyName,
                                 v.VacancyAddress,
                                 v.VacancyExp,
                                 v.VacancySalary,
                                 v.VacancyDescription,
                                 v.VacancyEmployerName,
                                 v.VacancyEmploymentType,
                                 v.VacancyRequirements
                             }).ToList();

            return Ok(dbvacancy);
        }

        [HttpGet("Is_Favourite")]
        public async Task<ActionResult<bool>> IsFavourite(int UserId, int VacancyId)
        {

            var dbFavourite = await _context.Favourites.FirstOrDefaultAsync(f => f.UserId == UserId && f.VacancyId == VacancyId);
            if (dbFavourite == null)
            {
                return Ok(false);
            }
            else
            {
                return Ok(true);
            }
        }

        [HttpGet("Is_Application")]
        public async Task<ActionResult<bool>> IsApplication(int UserId, int VacancyId)
        {

            var dbApplication = await _context.Applications.FirstOrDefaultAsync(f => f.UserId == UserId && f.VacancyId == VacancyId);
            if (dbApplication == null)
            {
                return Ok(false);
            }
            else
            {
                return Ok(true);
            }
        }
    }
}
