using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartmentController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetAllDepartment()
        {
            var Department = await _context.Departments.ToListAsync();

            return Ok(Department);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Department>> GetDepartmentById(int id)
        //{
        //    var c = await _context.Departments.FindAsync(id);
        //    if (c == null)
        //        return NotFound("Department not found.");

        //    return Ok(c);

        //}

        [HttpGet("{idCountry}")]
        public async Task<ActionResult<List<Department>>> GetDepartmentsByCountryId(int idCountry)
        {
            var departments = await _context.Departments
                                             .Where(d => d.CountryId == idCountry)
                                             .ToListAsync();

            if (departments == null || departments.Count == 0)
                return NotFound("Departments not found for the provided country id.");

            return Ok(departments);
        }


        [HttpPost]
        public async Task<ActionResult<List<Department>>> AddDepartment(Department Department)
        {
            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Department>> UpdateDepartment(Department updateDepartment)
        {
            var dbDepartment = await _context.Departments.FindAsync(updateDepartment.Id);
            if (dbDepartment == null)
                return NotFound("Department not found (put).");

            dbDepartment.Name = updateDepartment.Name;
            await _context.SaveChangesAsync();

            return Ok(await _context.Departments.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var dbDepartment = await _context.Departments.FindAsync(id);
            if (dbDepartment == null)
                return NotFound("Department not found (del).");

            _context.Departments.Remove(dbDepartment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Departments.ToListAsync());

        }


    }
}
