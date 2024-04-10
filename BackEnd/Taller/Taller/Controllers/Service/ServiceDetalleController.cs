using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            var ServiceDetalle = await _context.ServiceDetalles.ToListAsync();


            return Ok(ServiceDetalle);

            //public List<Vehicle> = new List<Vehicle>{};

            //var ServiceDetalle = await _context.ServiceDetalles
            //    .Include(sd => sd.Vehicle)
            //    .Select(sd => new EditServiceDetalle
            //    {
            //        // Mapea los datos que deseas devolver
            //        Id = sd.Id,

            //    })
            //    .ToListAsync();

            //return Ok(ServiceDetalle);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDetalle>> GetServiceDetalleById(int id)
        {
            var c = await _context.ServiceDetalles.FindAsync(id);
            if (c == null)
                return NotFound("ServiceDetalle not found.");

            return Ok(c);

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
