using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.DetalleWorkServicement
{
    [Route("taller/[controller]")]
    [ApiController]
    public class DetalleWSController : ControllerBase
    {
        private readonly DataContext _context;

        public DetalleWSController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<DetalleWorkService>>> GetAllDetalleWorkService()
        {
            var DetalleWorkService = await _context.DetalleWorkServices.ToListAsync();

            return Ok(DetalleWorkService);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleWorkService>> GetDetalleWorkServiceById(int id)
        {
            var c = await _context.DetalleWorkServices.FindAsync(id);
            if (c == null)
                return NotFound("DetalleWorkService not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<DetalleWorkService>>> AddDetalleWorkService(DetalleWorkService DetalleWorkService)
        {
            _context.DetalleWorkServices.Add(DetalleWorkService);
            await _context.SaveChangesAsync();
            return Ok(await _context.DetalleWorkServices.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<DetalleWorkService>> UpdateDetalleWorkService(DetalleWorkService updateDetalleWorkService)
        {
            var dbDetalleWorkService = await _context.DetalleWorkServices.FindAsync(updateDetalleWorkService.Id);
            if (dbDetalleWorkService == null)
                return NotFound("DetalleWorkService not found (put).");

            dbDetalleWorkService.ServiceWorkId = updateDetalleWorkService.ServiceWorkId;
            dbDetalleWorkService.ServiceDetalleId = updateDetalleWorkService.ServiceDetalleId;
            dbDetalleWorkService.InvoiceId = updateDetalleWorkService.InvoiceId;
            await _context.SaveChangesAsync();

            return Ok(await _context.DetalleWorkServices.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<DetalleWorkService>> DeleteDetalleWorkService(int id)
        {
            var dbDetalleWorkService = await _context.DetalleWorkServices.FindAsync(id);
            if (dbDetalleWorkService == null)
                return NotFound("DetalleWorkService not found (del).");

            _context.DetalleWorkServices.Remove(dbDetalleWorkService);
            await _context.SaveChangesAsync();

            return Ok(await _context.DetalleWorkServices.ToListAsync());

        }


    }
}
