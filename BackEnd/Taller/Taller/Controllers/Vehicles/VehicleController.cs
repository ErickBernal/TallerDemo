using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Vehicles
{
    [Route("taller/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DataContext _context;

        public VehicleController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetAllVehicle()
        {
            var Vehicle = await _context.Vehicles.ToListAsync();

            return Ok(Vehicle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
        {
            var c = await _context.Vehicles.FindAsync(id);
            if (c == null)
                return NotFound("Vehicle not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Vehicle>>> AddVehicle(Vehicle Vehicle)
        {
            _context.Vehicles.Add(Vehicle);
            await _context.SaveChangesAsync();
            return Ok(await _context.Vehicles.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Vehicle>> UpdateVehicle(Vehicle updateVehicle)
        {
            var dbVehicle = await _context.Vehicles.FindAsync(updateVehicle.Id);
            if (dbVehicle == null)
                return NotFound("Vehicle not found (put).");

            dbVehicle.Placa = updateVehicle.Placa;
            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var dbVehicle = await _context.Vehicles.FindAsync(id);
            if (dbVehicle == null)
                return NotFound("Vehicle not found (del).");

            _context.Vehicles.Remove(dbVehicle);
            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync());

        }


    }
}
