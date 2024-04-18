using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
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
        public async Task<ActionResult<List<VehiclePartsCompatible>>> GetAllVpcVehicle()
        {
            var vpartsCompatibles = await _context.VpartsCompatibles.ToListAsync();
            if (vpartsCompatibles == null) 
                return NotFound("No se encontraron VehiclePartsCompatible.");

            await _context.VpartsCompatibles.Include(x => x.VehiclePart).ToListAsync();
            await _context.VpartsCompatibles.Include(x => x.VehicleLinea).ToListAsync();
            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};
            //var jsonString = JsonSerializer.Serialize(vpartsCompatibles, options);

            var retList = new List<VehiclePartsCompatible>();
            foreach (var vpart in vpartsCompatibles)
            {
                var v = new VehiclePartsCompatible
                {
                    Id = vpart.Id,
                    VehicleLineaId = vpart.VehicleLineaId,
                    VehicleLinea = new VehicleLinea
                    {
                        Id = vpart.VehicleLinea.Id,
                        Color = vpart.VehicleLinea.Color,
                        Type = vpart.VehicleLinea.Type,
                        Line = vpart.VehicleLinea.Line,
                        VehicleModelId = vpart.VehicleLinea.VehicleModelId,
          
                    },
                    VehiclePartId = vpart.VehiclePartId,
                    VehiclePart = new VehiclePart
                    {
                        Id = vpart.VehiclePart.Id,
                        Name =vpart.VehiclePart.Name,
                        Stock = vpart.VehiclePart.Stock,
                        UnitPrice = vpart.VehiclePart.UnitPrice
                    }
                };
                retList.Add(v);
            }



            return Ok(retList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiclePartsCompatible>> GetVpcVehicleById(int id)
        {
            var c = await _context.VpartsCompatibles.FindAsync(id);
            if (c == null)
                return NotFound("Vehicle not found.");

            return Ok(c);
        }

        [HttpPost]
        public async Task<ActionResult<VehiclePartsCompatible>> AddVpcVehicle(VehiclePartsCompatible vehicle)
        {
            _context.VpartsCompatibles.Add(vehicle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVpcVehicleById), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVpcVehicle(int id, VehiclePartsCompatible updateVehicle)
        {
            if (id != updateVehicle.Id)
                return BadRequest("ID del vehículo no coincide con el cuerpo de la solicitud.");

            _context.Entry(updateVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                    return NotFound("Vehicle not found.");
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVpcVehicle(int id)
        {
            var vehicle = await _context.VpartsCompatibles.FindAsync(id);
            if (vehicle == null)
                return NotFound("Vehicle not found.");

            _context.VpartsCompatibles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.VpartsCompatibles.Any(e => e.Id == id);
        }
    }
}
