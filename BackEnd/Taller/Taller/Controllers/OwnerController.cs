using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.Location
{
    [Route("taller/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly DataContext _context;

        public OwnerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]


        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetLogById(int id)
        {
            var c = await _context.Owner.FindAsync(id);
            if (c == null)
                return NotFound("Owner not found.");

            return Ok(c);

        }



    }
}
