using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
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

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Municipality>> GetMunicipalityById(int id)
        //{
        //    var c = await _context.Municipalities.FindAsync(id);
        //    if (c == null)
        //        return NotFound("Municipality not found.");

        //    return Ok(c);

        //}


        [HttpGet("{idDepartment}")]
        public async Task<ActionResult<List<Department>>> GetDepartmentsByCountryId(int idDepartment)
        {
            var municipalities = await _context.Municipalities
                                             .Where(d => d.DepartmentId == idDepartment)
                                             .ToListAsync();

            if (municipalities == null || municipalities.Count == 0)
                return NotFound("Municipalities not found for the provided Department id.");

            return Ok(municipalities);
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
