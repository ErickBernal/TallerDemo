

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
            if (VehicleLinea == null)
                return NotFound("No se encontraron detalles de servicio.");

            await _context.VehicleLineas.Include(vl => vl.VehicleBrand).ToListAsync();
            await _context.VehicleLineas.Include(vl => vl.VehicleModel).ToListAsync();

            var returnVehicleLinea = new List<VehicleLinea>();
            foreach (var vehicle in VehicleLinea)
            {
                var v = new VehicleLinea
                {
                    Id = vehicle.Id,
                    Type = vehicle.Type,
                    Color = vehicle.Color,
                    Line = vehicle.Line,
                    VehicleModelId = vehicle.VehicleModelId,
                    VehicleModel = new VehicleModel
                    {
                        Id = vehicle.VehicleModel.Id,
                        Model = vehicle.VehicleModel.Model
                    },
                    VehicleBrandId = vehicle.VehicleBrandId,
                    VehicleBrand = new VehicleBrand
                    {
                        Id =vehicle.VehicleBrand.Id,
                        Brand = vehicle.VehicleBrand.Brand
                    }
                };
                returnVehicleLinea.Add(v);
            }
            return Ok(returnVehicleLinea);
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
            return Ok(VehicleLinea);
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
