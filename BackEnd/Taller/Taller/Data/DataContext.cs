using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Taller.Entities;
using TallerApi.Data.DefaultData;

namespace Taller.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }

        public DbSet<TypeClient> TypeClients { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleLinea> VehicleLineas { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehiclePart> VehicleParts { get; set; }
        public DbSet<VehiclePartsCompatible> VpartsCompatibles { get; set; }


        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ServiceWork> ServiceWorks { get; set; }
        public DbSet<ServiceListWork> ServiceListWorks { get; set; }
        public DbSet<ServiceDetalle> ServiceDetalles { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        //public DbSet<DetalleClientService> DetalleClientServices { get; set; }
        //public DbSet<DetalleWorkService> DetalleWorkServices { get; set; }
        public DbSet<DetalleVehicleParts> DetalleVehicleParts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<Country>()
            //    .HasMany(o => o.Departments)
            //    .WithOne(oi => oi.Country)
            //    .HasForeignKey(oi => oi.CountryId);
            //modelBuilder.Entity<Country>().HasData(DbInitializer.Prub());



            modelBuilder.Entity<DetalleVehicleParts>()
                .HasOne(dvp => dvp.ServiceDetalle)
                .WithMany(sd => sd.DetalleVehicleParts)
                .HasForeignKey(dvp => dvp.ServiceDetalleId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Country>().HasData(DbInitializer.GetCountriesToLoad());
            modelBuilder.Entity<Department>().HasData(DbInitializer.GetDepartmentsToLoad());
            modelBuilder.Entity<Municipality>().HasData(DbInitializer.GetMunicipalitiesToLoad());

            modelBuilder.Entity<TypeClient>().HasData(DbInitializer.GetTypeClientToLoad());

            modelBuilder.Entity<VehiclePart>().HasData(DbInitializer.GetVehiclePartsToLoad());
            modelBuilder.Entity<VehicleBrand>().HasData(DbInitializer.GetVehicleBrandToLoad());
            modelBuilder.Entity<VehicleModel>().HasData(DbInitializer.GetVehicleModelToLoad());
            modelBuilder.Entity<VehicleLinea>().HasData(DbInitializer.GetVehicleLineasToLoad());

            modelBuilder.Entity<ServiceType>().HasData(DbInitializer.GetTypeClientToLoad());
            modelBuilder.Entity<Invoice>().HasData(DbInitializer.GetInvoiceToLoad());
            modelBuilder.Entity<ServiceWork>().HasData(DbInitializer.GetServiceWorkToLoad());


            
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