using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Vehicles
{
    [Route("taller/[controller]")]
    [ApiController]
    public class VehiclePartsController : ControllerBase
    {
        private readonly DataContext _context;

        public VehiclePartsController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<VehiclePart>>> GetAllVehicle()
        {
            var Vehicle = await _context.VehicleParts.ToListAsync();

            return Ok(Vehicle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiclePart>> GetVehicleById(int id)
        {
            var c = await _context.VehicleParts.FindAsync(id);
            if (c == null)
                return NotFound("Vehicle not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<VehiclePart>>> AddVehicle(VehiclePart Vehicle)
        {
            _context.VehicleParts.Add(Vehicle);
            await _context.SaveChangesAsync();
            return Ok(await _context.VehicleParts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<VehiclePart>> UpdateVehicle(VehiclePart updateVehicle)
        {
            var dbVehicle = await _context.VehicleParts.FindAsync(updateVehicle.Id);
            if (dbVehicle == null)
                return NotFound("Vehicle not found (put).");

            dbVehicle.Name = updateVehicle.Name;
            dbVehicle.Stock = updateVehicle.Stock;
            dbVehicle.UnitPrice = updateVehicle.UnitPrice;
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleParts.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<VehiclePart>> DeleteVehicle(int id)
        {
            var dbVehicle = await _context.VehicleParts.FindAsync(id);
            if (dbVehicle == null)
                return NotFound("Vehicle not found (del).");

            _context.VehicleParts.Remove(dbVehicle);
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleParts.ToListAsync());

        }


    }
}
