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
    public class LogsController : ControllerBase
    {
        private readonly DataContext _context;
        public LogsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Log>>> GetAllLog()
        {
            var Log = await _context.Logs.ToListAsync();

            return Ok(Log);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLogById(int id)
        {
            var c = await _context.Logs.FindAsync(id);
            if (c == null)
                return NotFound("Log not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Log>>> AddLog(Log Log)
        {
            _context.Logs.Add(Log);
            await _context.SaveChangesAsync();
            return Ok(await _context.Logs.ToListAsync());
        }
    }
}
