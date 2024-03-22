using Microsoft.EntityFrameworkCore;
using Backend_taller.Models;

namespace Backend_taller.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }

    }

}
