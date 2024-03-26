using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TallerApi.Data.DefaultData;
using TallerApi.Models;

namespace TallerApi.DataAccess
{
    public class TallerDbContext : DbContext
    {
        public TallerDbContext(DbContextOptions<TallerDbContext> options) : base(options)
        { }

        public DbSet<Country> Country { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Municipality> Municipality { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(DefaultData.GetCountriesToLoad());

        }

    }
}



