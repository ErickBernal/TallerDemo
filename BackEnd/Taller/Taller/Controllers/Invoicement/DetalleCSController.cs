using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.DetalleClientServicement
{
    [Route("taller/[controller]")]
    [ApiController]
    public class DetalleCSController : ControllerBase
    {
        private readonly DataContext _context;

        public DetalleCSController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<DetalleClientService>>> GetAllDetalleClientService()
        {
            var DetalleClientService = await _context.DetalleClientServices.ToListAsync();

            return Ok(DetalleClientService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleClientService>> GetDetalleClientServiceById(int id)
        {
            var c = await _context.DetalleClientServices.FindAsync(id);
            if (c == null)
                return NotFound("DetalleClientService not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<DetalleClientService>>> AddDetalleClientService(DetalleClientService DetalleClientService)
        {
            _context.DetalleClientServices.Add(DetalleClientService);
            await _context.SaveChangesAsync();
            return Ok(await _context.DetalleClientServices.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<DetalleClientService>> UpdateDetalleClientService(DetalleClientService updateDetalleClientService)
        {
            var dbDetalleClientService = await _context.DetalleClientServices.FindAsync(updateDetalleClientService.Id);
            if (dbDetalleClientService == null)
                return NotFound("DetalleClientService not found (put).");

            dbDetalleClientService.ClientId = updateDetalleClientService.ClientId;
            dbDetalleClientService.ServiceDetalleId = updateDetalleClientService.ServiceDetalleId;
            dbDetalleClientService.InvoiceId = updateDetalleClientService.InvoiceId;
            await _context.SaveChangesAsync();

            return Ok(await _context.DetalleClientServices.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<DetalleClientService>> DeleteDetalleClientService(int id)
        {
            var dbDetalleClientService = await _context.DetalleClientServices.FindAsync(id);
            if (dbDetalleClientService == null)
                return NotFound("DetalleClientService not found (del).");

            _context.DetalleClientServices.Remove(dbDetalleClientService);
            await _context.SaveChangesAsync();

            return Ok(await _context.DetalleClientServices.ToListAsync());

        }


    }
}
