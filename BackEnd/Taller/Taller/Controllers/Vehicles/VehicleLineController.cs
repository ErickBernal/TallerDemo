

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Vehicles
{
    [Route("taller/[controller]")]
    [ApiController]
    public class VehicleLineController : ControllerBase
    {
        private readonly DataContext _context;

        public VehicleLineController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<VehicleLinea>>> GetAllVehicleLinea()
        {
            var VehicleLinea = await _context.VehicleLineas.ToListAsync();

            return Ok(VehicleLinea);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleLinea>> GetVehicleLineaById(int id)
        {
            var c = await _context.VehicleLineas.FindAsync(id);
            if (c == null)
                return NotFound("VehicleLinea not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<VehicleLinea>>> AddVehicleLinea(VehicleLinea VehicleLinea)
        {
            _context.VehicleLineas.Add(VehicleLinea);
            await _context.SaveChangesAsync();
            return Ok(await _context.VehicleLineas.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<VehicleLinea>> UpdateVehicleLinea(VehicleLinea updateVehicleLinea)
        {
            var dbVehicleLinea = await _context.VehicleLineas.FindAsync(updateVehicleLinea.Id);
            if (dbVehicleLinea == null)
                return NotFound("VehicleLinea not found (put).");

            dbVehicleLinea.Color = updateVehicleLinea.Color;
            dbVehicleLinea.Type = updateVehicleLinea.Type;
            dbVehicleLinea.Line = updateVehicleLinea.Line;

            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleLineas.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<VehicleLinea>> DeleteVehicleLinea(int id)
        {
            var dbVehicleLinea = await _context.VehicleLineas.FindAsync(id);
            if (dbVehicleLinea == null)
                return NotFound("VehicleLinea not found (del).");

            _context.VehicleLineas.Remove(dbVehicleLinea);
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleLineas.ToListAsync());

        }


    }
}
