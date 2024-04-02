using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly DataContext _context;

        public CountryController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAllCountry()
        {
            var country = await _context.Countries.ToListAsync();

            return Ok(country);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            var c = await _context.Countries.FindAsync(id);
            if (c == null)
                return NotFound("Country not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Country>>> AddCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return Ok(await _context.Countries.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Country>> UpdateCountry(Country updateCountry)
        {
            var dbCountry = await _context.Countries.FindAsync(updateCountry.Id);
            if (dbCountry == null)
                return NotFound("Country not found (put).");

            dbCountry.Name = updateCountry.Name;
            await _context.SaveChangesAsync();

            return Ok(await _context.Countries.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var dbCountry = await _context.Countries.FindAsync(id);
            if (dbCountry == null)
                return NotFound("Country not found (del).");

            _context.Countries.Remove(dbCountry);
            await _context.SaveChangesAsync();

            return Ok(await _context.Countries.ToListAsync());

        }


    }
}
