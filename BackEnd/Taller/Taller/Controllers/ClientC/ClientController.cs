using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.ClientC
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
            if (Client == null)
                return NotFound("No se encontraron detalles del cliente.");

            var typeClient = await _context.Clients.Include(t => t.TypeClient).ToListAsync();
            var muni = await _context.Clients.Include(m => m.Municipality).ToListAsync();
            await _context.Municipalities.Include(d => d.Department).ToListAsync();
            await _context.Departments.Include(c => c.Country).ToListAsync();

            //build response
            var clientsToReturn = new List<Client>();
            foreach (var client in Client)
            {
                var c = new Client
                {
                    Id = client.Id,
                    Name = client.Name,
                    LastName = client.LastName,
                    DPI = client.DPI,
                    Nit = client.Nit,
                    Phone = client.Phone,
                    Cellphone = client.Cellphone,
                    TypeClientId = client.TypeClientId,
                    Zone = client.Zone,
                    Address = client.Address,
                    MunicipalityId = client.MunicipalityId,
                    ServiceDetalles = null,
                    TypeClient = new TypeClient
                    {
                        Id = client.TypeClient.Id,
                        Type = client.TypeClient.Type,
                        Clients = null
                    },
                    Municipality = new Municipality
                    {
                        Id = client.Municipality.Id,
                        Name= client.Municipality.Name,
                        DepartmentId = client.Municipality.DepartmentId,
                        Department = new Department
                        {
                            Id = client.Municipality.Department.Id,
                            Name = client.Municipality.Department.Name,
                            CountryId = client.Municipality.Department.CountryId,
                            Country = new Country {
                                Id = client.Municipality.Department.Country.Id,
                                Name = client.Municipality.Department.Country.Name
                            },
                        },
                    },
                };
                clientsToReturn.Add(c);
            }
            return Ok(clientsToReturn);
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
            dbClient.DPI = updateClient.DPI;
            dbClient.Nit = updateClient.Nit;
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
