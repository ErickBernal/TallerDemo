using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller.Data;
using Taller.Entities;

namespace Taller.Controllers.ClientC
{
    [Route("taller/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly DataContext _context;

        public AddressController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAddress()
        {
            var Address = await _context.Addresses.ToListAsync();

            return Ok(Address);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(int id)
        {
            var c = await _context.Addresses.FindAsync(id);
            if (c == null)
                return NotFound("Address not found.");

            return Ok(c);

        }

        [HttpPost]
        public async Task<ActionResult<List<Address>>> AddAddress(Address Address)
        {
            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();
            return Ok(await _context.Addresses.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Address>> UpdateAddress(Address updateAddress)
        {
            var dbAddress = await _context.Addresses.FindAsync(updateAddress.Id);
            if (dbAddress == null)
                return NotFound("Address not found (put).");

            dbAddress.Description = updateAddress.Description;
            dbAddress.Zone = updateAddress.Zone;
            await _context.SaveChangesAsync();

            return Ok(await _context.Addresses.ToListAsync());

        }

        [HttpDelete]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var dbAddress = await _context.Addresses.FindAsync(id);
            if (dbAddress == null)
                return NotFound("Address not found (del).");

            _context.Addresses.Remove(dbAddress);
            await _context.SaveChangesAsync();

            return Ok(await _context.Addresses.ToListAsync());

        }


    }
}
