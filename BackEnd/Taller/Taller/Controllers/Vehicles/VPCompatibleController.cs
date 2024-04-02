using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Vehicles
{
    [Route("taller/[controller]")]
    [ApiController]
    public class VPCompatibleController : ControllerBase
    {
        private readonly DataContext _context;

        public VPCompatibleController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<VehiclePartsCompatible>>> GetAllVehicle()
        {
            var Vehicle = await _context.VpartsCompatibles.ToListAsync();

            return Ok(Vehicle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiclePartsCompatible>> GetVehicleById(int id)
        {
            var c = await _context.VpartsCompatibles.FindAsync(id);
            if (c == null)
                return NotFound("Vehicle not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<VehiclePartsCompatible>>> AddVehicle(VehiclePartsCompatible Vehicle)
        {
            _context.VpartsCompatibles.Add(Vehicle);
            await _context.SaveChangesAsync();
            return Ok(await _context.VpartsCompatibles.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<VehiclePartsCompatible>> UpdateVehicle(VehiclePartsCompatible updateVehicle)
        {
            var dbVehicle = await _context.VpartsCompatibles.FindAsync(updateVehicle.Id);
            if (dbVehicle == null)
                return NotFound("Vehicle not found (put).");

            dbVehicle.VehicleLinea = updateVehicle.VehicleLinea;
            dbVehicle.VehiclePart = updateVehicle.VehiclePart;
            await _context.SaveChangesAsync();

            return Ok(await _context.VpartsCompatibles.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<VehiclePartsCompatible>> DeleteVehicle(int id)
        {
            var dbVehicle = await _context.VpartsCompatibles.FindAsync(id);
            if (dbVehicle == null)
                return NotFound("Vehicle not found (del).");

            _context.VpartsCompatibles.Remove(dbVehicle);
            await _context.SaveChangesAsync();

            return Ok(await _context.VpartsCompatibles.ToListAsync());

        }


    }
}

