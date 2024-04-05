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

        [HttpPost]
        public async Task<ActionResult<List<Owner>>> AddCountry(Owner ow)
        {
            Console.WriteLine("------- woner -------");

            var isOwner = await _context.Owner.FindAsync(1);
            if (isOwner == null)
                return NotFound("Owner, no hay datos en DB");

            if (isOwner.Pwd == ow.Pwd)
                return Ok(true);

            //return NotFound("Owner, No coincide la contraseña");
            return Unauthorized();
        }

    }
}
