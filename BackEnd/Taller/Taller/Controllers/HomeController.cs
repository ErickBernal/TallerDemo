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
    public class HomeController : ControllerBase
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Login>>> GetAllLogin()
        {
            var Login = await _context.logins.ToListAsync();

            return Ok(Login);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLoginById(int id)
        {
            var c = await _context.logins.FindAsync(id);
            if (c == null)
                return NotFound("Login not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Login>>> AddLogin(Login Login)
        {
            _context.logins.Add(Login);
            await _context.SaveChangesAsync();
            return Ok(await _context.logins.ToListAsync());
        }
    }
}
