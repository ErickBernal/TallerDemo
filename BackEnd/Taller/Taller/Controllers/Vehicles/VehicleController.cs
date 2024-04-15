using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
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
            if (Vehicle == null)
                return NotFound("No se encontro vehiculos (get)");

            //cargando la linea de cada vehiculo
            await _context.Vehicles.Include(v => v.VehicleLinea).ToListAsync();

            var vehiclesToReturn = new List<Vehicle>();
            foreach (var v in Vehicle)
            {
                var vehicle = new Vehicle
                {
                    Id = v.Id,
                    Placa = v.Placa,
                    VehicleLineaId = v.VehicleLineaId,
                    VehicleLinea = new VehicleLinea
                    {
                        Id = v.VehicleLinea.Id,
                        Color = v.VehicleLinea.Color,
                        Type = v.VehicleLinea.Type,
                        Line = v.VehicleLinea.Line,
                    }
                };

                vehiclesToReturn.Add(vehicle);
            }

            return Ok(vehiclesToReturn);
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
            dbVehicle.VehicleLineaId = updateVehicle.VehicleLineaId;
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
