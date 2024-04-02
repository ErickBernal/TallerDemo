using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Vehicles
{
    [Route("taller/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly DataContext _context;

        public VehicleModelController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<VehicleModel>>> GetAllVehicleModel()
        {
            var VehicleModel = await _context.VehicleModels.ToListAsync();

            return Ok(VehicleModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleModelById(int id)
        {
            var c = await _context.VehicleModels.FindAsync(id);
            if (c == null)
                return NotFound("VehicleModel not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<VehicleModel>>> AddVehicleModel(VehicleModel VehicleModel)
        {
            _context.VehicleModels.Add(VehicleModel);
            await _context.SaveChangesAsync();
            return Ok(await _context.VehicleModels.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<VehicleModel>> UpdateVehicleModel(VehicleModel updateVehicleModel)
        {
            var dbVehicleModel = await _context.VehicleModels.FindAsync(updateVehicleModel.Id);
            if (dbVehicleModel == null)
                return NotFound("VehicleModel not found (put).");

            dbVehicleModel.Model = updateVehicleModel.Model;
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleModels.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<VehicleModel>> DeleteVehicleModel(int id)
        {
            var dbVehicleModel = await _context.VehicleModels.FindAsync(id);
            if (dbVehicleModel == null)
                return NotFound("VehicleModel not found (del).");

            _context.VehicleModels.Remove(dbVehicleModel);
            await _context.SaveChangesAsync();

            return Ok(await _context.VehicleModels.ToListAsync());

        }


    }
}
