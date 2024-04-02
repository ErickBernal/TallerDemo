using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Invoicement
{
    [Route("taller/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoiceController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Invoice>>> GetAllInvoice()
        {
            var Invoice = await _context.Invoices.ToListAsync();

            return Ok(Invoice);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int id)
        {
            var c = await _context.Invoices.FindAsync(id);
            if (c == null)
                return NotFound("Invoice not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Invoice>>> AddInvoice(Invoice Invoice)
        {
            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();
            return Ok(await _context.Invoices.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Invoice>> UpdateInvoice(Invoice updateInvoice)
        {
            var dbInvoice = await _context.Invoices.FindAsync(updateInvoice.Id);
            if (dbInvoice == null)
                return NotFound("Invoice not found (put).");

            dbInvoice.DateTime = updateInvoice.DateTime;
            dbInvoice.Value = updateInvoice.Value;
            await _context.SaveChangesAsync();

            return Ok(await _context.Invoices.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Invoice>> DeleteInvoice(int id)
        {
            var dbInvoice = await _context.Invoices.FindAsync(id);
            if (dbInvoice == null)
                return NotFound("Invoice not found (del).");

            _context.Invoices.Remove(dbInvoice);
            await _context.SaveChangesAsync();

            return Ok(await _context.Invoices.ToListAsync());

        }


    }
}
