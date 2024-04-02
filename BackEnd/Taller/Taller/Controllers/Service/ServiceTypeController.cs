using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceTypeController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<ServiceType>>> GetAllServiceType()
        {
            var ServiceType = await _context.ServiceTypes.ToListAsync();

            return Ok(ServiceType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceType>> GetServiceTypeById(int id)
        {
            var c = await _context.ServiceTypes.FindAsync(id);
            if (c == null)
                return NotFound("ServiceType not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<ServiceType>>> AddServiceType(ServiceType ServiceType)
        {
            _context.ServiceTypes.Add(ServiceType);
            await _context.SaveChangesAsync();
            return Ok(await _context.ServiceTypes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceType>> UpdateServiceType(ServiceType updateServiceType)
        {
            var dbServiceType = await _context.ServiceTypes.FindAsync(updateServiceType.Id);
            if (dbServiceType == null)
                return NotFound("ServiceType not found (put).");

            dbServiceType.Type = updateServiceType.Type;
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceTypes.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<ServiceType>> DeleteServiceType(int id)
        {
            var dbServiceType = await _context.ServiceTypes.FindAsync(id);
            if (dbServiceType == null)
                return NotFound("ServiceType not found (del).");

            _context.ServiceTypes.Remove(dbServiceType);
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceTypes.ToListAsync());

        }


    }
}
