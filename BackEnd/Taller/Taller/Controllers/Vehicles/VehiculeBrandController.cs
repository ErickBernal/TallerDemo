

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Vehicles
{
    [Route("taller/[controller]")]
    [ApiController]
    public class VehiculeBrandController : ControllerBase
    {
        private readonly DataContext _context;

        public VehiculeBrandController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<VehicleBrand>>> GetAllVehicleBrand()
        {
            var VehicleBrand = await _context.VehicleBrands.ToListAsync();

            return Ok(VehicleBrand);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleBrand>> GetVehicleBrandById(int id)
        {
            var c = await _context.VehicleBrands.FindAsync(id);
            if (c == null)
                return NotFound("VehicleBrand not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<VehicleBrand>>> AddVehicleBrand(VehicleBrand VehicleBrand)
        {
            _context.VehicleBrands.Add(VehicleBrand);
            await _context.SaveChangesAsync();
            return Ok(await _context.VehicleBrands.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<VehicleBrand>> UpdateVehicleBrand(VehicleBrand updateVehicleBrand)
        {
            var dbVehicleBrand = await _context.VehicleBrands.FindAsync(updateVehicleBrand.Id);
            if (dbVehicleBrand == null)
                return NotFound("VehicleBrand not found (put).");

            dbVehicleBrand.Brand = updateVehicleBrand.Brand;
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleBrands.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<VehicleBrand>> DeleteVehicleBrand(int id)
        {
            var dbVehicleBrand = await _context.VehicleBrands.FindAsync(id);
            if (dbVehicleBrand == null)
                return NotFound("VehicleBrand not found (del).");

            _context.VehicleBrands.Remove(dbVehicleBrand);
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleBrands.ToListAsync());

        }


    }
}
