using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Text.Json.Serialization;
using System.Text.Json;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class ServiceDetalleController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceDetalleController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<ServiceDetalle>>> GetAllServiceDetalle()
        {
            //var serviceDetalles = await _context.ServiceDetalles.Include(sd => sd.Client).ToListAsync();
            //if (serviceDetalles == null)
            //    return NotFound("No se encontraron detalles de servicio.");

            //return Ok(serviceDetalles);



            var serviceDetalles = await _context.ServiceDetalles
                .Include(sd => sd.Client)
                .ToListAsync();
            await _context.ServiceDetalles
               .Include(sd => sd.Vehicle)
               .ToListAsync();

            if (serviceDetalles == null)
                return NotFound("No se encontraron detalles de servicio.");

            var serviceDetalleDtos = new List<ServiceDetalle>();
            foreach (var detalle in serviceDetalles)
            {
                var dto = new ServiceDetalle
                {
                    Id = detalle.Id,
                    ClientId = detalle.ClientId,
                    State = detalle.State,
                    Km = detalle.Km,
                    ServiceType = detalle.ServiceType,
                    ServiceTypeId = detalle.ServiceTypeId,
                    VehicleId = detalle.Vehicle.Id,
                    Vehicle = new Vehicle
                    {
                        Id = detalle.Vehicle.Id,
                        Placa = detalle.Vehicle.Placa,
                        VehicleLineaId = detalle.Vehicle.VehicleLineaId,
                        //VehicleLinea = await _context.VehicleLineas.FindAsync(detalle.Vehicle.VehicleLineaId)
                    },

                    Client = new Client
                    {
                        Id = detalle.Client.Id,
                        Name = detalle.Client.Name,
                        LastName = detalle.Client.LastName,
                    }
                };
                serviceDetalleDtos.Add(dto);
            }

            return Ok(serviceDetalleDtos);
        }

        [HttpGet("{idVehicle}")]
        public async Task<ActionResult<ServiceDetalle>> GetServiceDetalleById(int idVehicle)
        {
            //var c = await _context.ServiceDetalles.FindAsync(idVehicle);
            var serviceDetalle = await _context.ServiceDetalles.FirstOrDefaultAsync(x => x.VehicleId == idVehicle);
            if (serviceDetalle != null)
            {
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == serviceDetalle.ClientId);
                if (client == null)
                    return NotFound("GetServiceDetalleById/client not found.");
                //serviceDetalle.Client = client;

                var servicesType = await _context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == serviceDetalle.ServiceTypeId);


                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == serviceDetalle.VehicleId);
                if (vehicle == null)
                    return NotFound("GetServiceDetalleById/vehicle not found.");

                var typeClient = await _context.TypeClients.FirstOrDefaultAsync(x => x.Id == client.TypeClientId);


                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var jsonString = JsonSerializer.Serialize(serviceDetalle, options);


                return Ok(jsonString);
            }

            if (serviceDetalle == null)
                return NotFound("ServiceDetalle by idVehicle not found.");

            return NotFound("ServiceDetalleById not found.");
        }

        [HttpPost]
        public async Task<ActionResult<List<ServiceDetalle>>> AddServiceDetalle(ServiceDetalle ServiceDetalle)
        {
            _context.ServiceDetalles.Add(ServiceDetalle);
            await _context.SaveChangesAsync();
            return Ok(await _context.ServiceDetalles.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceDetalle>> UpdateServiceDetalle(ServiceDetalle updateServiceDetalle)
        {
            var dbServiceDetalle = await _context.ServiceDetalles.FindAsync(updateServiceDetalle.Id);
            if (dbServiceDetalle == null)
                return NotFound("ServiceDetalle not found (put).");

            dbServiceDetalle.State = updateServiceDetalle.State;
            dbServiceDetalle.Km = updateServiceDetalle.Km;
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceDetalles.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<ServiceDetalle>> DeleteServiceDetalle(int id)
        {
            var dbServiceDetalle = await _context.ServiceDetalles.FindAsync(id);
            if (dbServiceDetalle == null)
                return NotFound("ServiceDetalle not found (del).");

            _context.ServiceDetalles.Remove(dbServiceDetalle);
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceDetalles.ToListAsync());

        }


    }
}
