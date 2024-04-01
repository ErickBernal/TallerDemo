using Microsoft.EntityFrameworkCore;
using Taller.Entities;
using TallerApi.Data.DefaultData;

namespace Taller.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Department> Departments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(o => o.Departments)
                .WithOne(oi => oi.Country)
                .HasForeignKey(oi => oi.CountryId);

            modelBuilder.Entity<Country>().HasData(DbInitializer.GetCountriesToLoad());
            modelBuilder.Entity<Department>().HasData(DbInitializer.GetDepartmentsToLoad());
            modelBuilder.Entity<Municipality>().HasData(DbInitializer.GetMunicipalitiesToLoad());


        }

    }
}



//modelBuilder.Entity<Country>().HasData(DefaultData.GetCountriesToLoad());
//public override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Order>()
//        .HasMany(o => o.OrderItems)             // Order has many OrderItems
//        .WithOne(oi => oi.Order)                 // OrderItem has one Order
//        .HasForeignKey(oi => oi.OrderId);        // Foreign key is OrderId in OrderItem
//}