using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class ServiceWorkController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceWorkController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<ServiceWork>>> GetAllServiceWork()
        {
            var ServiceWork = await _context.ServiceWorks.ToListAsync();

            return Ok(ServiceWork);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceWork>> GetServiceWorkById(int id)
        {
            var c = await _context.ServiceWorks.FindAsync(id);
            if (c == null)
                return NotFound("ServiceWork not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<ServiceWork>>> AddServiceWork(ServiceWork ServiceWork)
        {
            _context.ServiceWorks.Add(ServiceWork);
            await _context.SaveChangesAsync();
            return Ok(await _context.ServiceWorks.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceWork>> UpdateServiceWork(ServiceWork updateServiceWork)
        {
            var dbServiceWork = await _context.ServiceWorks.FindAsync(updateServiceWork.Id);
            if (dbServiceWork == null)
                return NotFound("ServiceWork not found (put).");

            dbServiceWork.Work = updateServiceWork.Work;
            dbServiceWork.PriceWork = updateServiceWork.PriceWork;
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceWorks.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<ServiceWork>> DeleteServiceWork(int id)
        {
            var dbServiceWork = await _context.ServiceWorks.FindAsync(id);
            if (dbServiceWork == null)
                return NotFound("ServiceWork not found (del).");

            _context.ServiceWorks.Remove(dbServiceWork);
            await _context.SaveChangesAsync();

            return Ok(await _context.ServiceWorks.ToListAsync());

        }


    }
}
