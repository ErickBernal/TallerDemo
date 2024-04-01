using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers
{
    [Route("taller/[controller]")]
    [ApiController]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public MunicipalitiesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Municipality>>> GetAllMunicipality()
        {
            var Municipality = await _context.Municipalities.ToListAsync();

            return Ok(Municipality);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetMunicipalityById(int id)
        {
            var c = await _context.Municipalities.FindAsync(id);
            if (c == null)
                return NotFound("Municipality not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Municipality>>> AddMunicipality(Municipality Municipality)
        {
            _context.Municipalities.Add(Municipality);
            await _context.SaveChangesAsync();
            return Ok(await _context.Municipalities.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Municipality>> UpdateMunicipality(Municipality updateMunicipality)
        {
            var dbMunicipality = await _context.Municipalities.FindAsync(updateMunicipality.Id);
            if (dbMunicipality == null)
                return NotFound("Municipality not found (put).");

            dbMunicipality.Name = updateMunicipality.Name;
            await _context.SaveChangesAsync();

            return Ok(await _context.Municipalities.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Municipality>> DeleteMunicipality(int id)
        {
            var dbMunicipality = await _context.Municipalities.FindAsync(id);
            if (dbMunicipality == null)
                return NotFound("Municipality not found (del).");

            _context.Municipalities.Remove(dbMunicipality);
            await _context.SaveChangesAsync();

            return Ok(await _context.Municipalities.ToListAsync());

        }


    }
}
