﻿using Findmaster.DataAccessLayer.Entity;
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
            var vacancies = _context.Vacancies.ToList().OrderBy(v => v.VacancyId);
            return Ok(vacancies);
        }

        [HttpPost("Add_Vacancy")]
        public async Task<IActionResult> AddVacancies(string VacancyName, int VacancySalary, string VacancyEmployerName,string VacancyAddress,string VacancyRequirements,string VacancyExp, string VacancyEmploymentType,string VacancyDescription, int UserId)
        {
            _context.Vacancies.Add(new Vacancy(VacancyName, VacancySalary, VacancyEmployerName, VacancyAddress, VacancyRequirements, VacancyExp, VacancyEmploymentType, VacancyDescription));
            _context.SaveChanges();
            var lastvacantion = await _context.Vacancies.OrderByDescending(v => v.VacancyId).FirstOrDefaultAsync();

            _context.Applications.Add(new Applications(UserId, lastvacantion.VacancyId));
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

        [HttpPost("Add_Favourite")]
        public async Task<IActionResult> AddFavourite(int UserId, int VacancyId)
        {
            
            _context.Favourites.Add(new Favourite(UserId,VacancyId));
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Delete_Favourite")]
        public async Task<IActionResult> DeleteFavourite(int UserId, int VacancyId)
        {

            var dbfavourite = _context.Favourites.Where(f => f.UserId == UserId && f.VacancyId == VacancyId).FirstOrDefault();
            _context.Favourites.Remove(dbfavourite);
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
        [HttpGet("Get_Employer_Id")]
        public async Task<ActionResult<int>> GetEmployerId(int VacancyId)
        {
            var dbUser = _context.Applications.FirstOrDefault(a => a.VacancyId == VacancyId);
            if(dbUser == null)
            {
                return BadRequest("Application doesn't exist");
            }
            return Ok(dbUser.UserId);
        }
    }
}
