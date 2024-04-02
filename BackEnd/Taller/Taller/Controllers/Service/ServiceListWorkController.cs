using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class ServiceListWorkController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceListWorkController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<ServiceListWork>>> GetAllServiceListWork()
        {
            var ServiceListWork = await _context.ServiceListWorks.ToListAsync();

            return Ok(ServiceListWork);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceListWork>> GetServiceListWorkById(int id)
        {
            var c = await _context.ServiceListWorks.FindAsync(id);
            if (c == null)
                return NotFound("ServiceListWork not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<ServiceListWork>>> AddServiceListWork(ServiceListWork ServiceListWork)
        {
            _context.ServiceListWorks.Add(ServiceListWork);
            await _context.SaveChangesAsync();
            return Ok(await _context.ServiceListWorks.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceListWork>> UpdateServiceListWork(ServiceListWork updateServiceListWork)
        {
            var dbServiceListWork = await _context.ServiceListWorks.FindAsync(updateServiceListWork.Id);
            if (dbServiceListWork == null)
                return NotFound("ServiceListWork not found (put).");

            dbServiceListWork.ServiceTypeId = updateServiceListWork.ServiceTypeId;
            dbServiceListWork.ServiceWorkId = updateServiceListWork.ServiceWorkId;

            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceListWorks.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<ServiceListWork>> DeleteServiceListWork(int id)
        {
            var dbServiceListWork = await _context.ServiceListWorks.FindAsync(id);
            if (dbServiceListWork == null)
                return NotFound("ServiceListWork not found (del).");

            _context.ServiceListWorks.Remove(dbServiceListWork);
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceListWorks.ToListAsync());

        }


    }
}
