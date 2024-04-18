using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.DetalleVehiclePartsment
{
    [Route("taller/[controller]")]
    [ApiController]
    public class DetalleVPController : ControllerBase
    {
        private readonly DataContext _context;

        public DetalleVPController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<DetalleVehicleParts>>> GetAllDetalleVehicleParts()
        {
            var DetalleVehicleParts = await _context.DetalleVehicleParts .ToListAsync();

            return Ok(DetalleVehicleParts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVehicleParts>> GetDetalleVehiclePartsById(int id)
        {
            var c = await _context.DetalleVehicleParts.FindAsync(id);
            if (c == null)
                return NotFound("DetalleVehicleParts not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<DetalleVehicleParts>>> AddDetalleVehicleParts(DetalleVehicleParts DetalleVehicleParts)
        {
            _context.DetalleVehicleParts.Add(DetalleVehicleParts);
            await _context.SaveChangesAsync();
            return Ok(await _context.DetalleVehicleParts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<DetalleVehicleParts>> UpdateDetalleVehicleParts(DetalleVehicleParts updateDetalleVehicleParts)
        {
            var dbDetalleVehicleParts = await _context.DetalleVehicleParts.FindAsync(updateDetalleVehicleParts.Id);
            if (dbDetalleVehicleParts == null)
                return NotFound("DetalleVehicleParts not found (put).");

            dbDetalleVehicleParts.VehiclePartId = updateDetalleVehicleParts.VehiclePartId;
            dbDetalleVehicleParts.ServiceDetalleId = updateDetalleVehicleParts.ServiceDetalleId;
            await _context.SaveChangesAsync();

            return Ok(await _context.DetalleVehicleParts.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<DetalleVehicleParts>> DeleteDetalleVehicleParts(int id)
        {
            var dbDetalleVehicleParts = await _context.DetalleVehicleParts.FindAsync(id);
            if (dbDetalleVehicleParts == null)
                return NotFound("DetalleVehicleParts not found (del).");

            _context.DetalleVehicleParts.Remove(dbDetalleVehicleParts);
            await _context.SaveChangesAsync();

            return Ok(await _context.DetalleVehicleParts.ToListAsync());

        }


    }
}
