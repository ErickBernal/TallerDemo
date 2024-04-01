using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers
{
    [Route("taller/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClient()
        {
            var Client = await _context.Clients.ToListAsync();

            return Ok(Client);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var c = await _context.Clients.FindAsync(id);
            if (c == null)
                return NotFound("Client not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Client>>> AddClient(Client Client)
        {
            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Client>> UpdateClient(Client updateClient)
        {
            var dbClient = await _context.Clients.FindAsync(updateClient.Id);
            if (dbClient == null)
                return NotFound("Client not found (put).");

            dbClient.Name = updateClient.Name;
            dbClient.LastName = updateClient.LastName;
            dbClient.DPI  = updateClient.DPI;
            dbClient.Nit  = updateClient.Nit;
            dbClient.Phone = updateClient.Phone;
            dbClient.Cellphone = updateClient.Cellphone;
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var dbClient = await _context.Clients.FindAsync(id);
            if (dbClient == null)
                return NotFound("Client not found (del).");

            _context.Clients.Remove(dbClient);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());

        }


    }
}
