
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers
{
    [Route("taller/[controller]")]
    [ApiController]
    public class TypeClientController : ControllerBase
    {
        private readonly DataContext _context;

        public TypeClientController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TypeClient>>> GetAllTypeClient()
        {
            var x = await _context.TypeClients.ToListAsync();
            return Ok(x);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeClient>> GetTypeClientById(int id)
        {
            var c = await _context.TypeClients.FindAsync(id);
            if (c == null)
                return NotFound("TypeClient not found.");

            return Ok(c);

        }

        //[HttpPost]
        //public async Task<ActionResult<List<TypeClient>>> AddCountry(TypeClient x)
        //{
        //    _context.TypeClients.Add(x);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.TypeClients.ToListAsync());
        //}

    }
}
