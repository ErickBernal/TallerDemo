
using System.ComponentModel.DataAnnotations;

namespace Taller.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Value { get; set; }

        public ICollection<DetalleVehicleParts>? DetalleVehicleParts { get; set; }
        public ICollection<ServiceListWork>? ServiceListWork { get; set; }

    }


    //public class DetalleWorkService
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    public int ServiceWorkId { get; set; }
    //    public ServiceWork? ServiceWork { get; set; }

    //    public int ServiceDetalleId { get; set; }
    //    public ServiceDetalle? ServiceDetalle { get; set; }

    //    public ICollection<Invoice>? Invoices { get; set; }

    //}

    public class DetalleVehicleParts
    {
        [Key]
        public int Id { get; set; }

        public int VehiclePartId { get; set; }
        public VehiclePart? VehiclePart { get; set; }
        
        public int ServiceDetalleId { get; set; }
        public ServiceDetalle? ServiceDetalle { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set;}
        //public ICollection<Invoice>? Invoices { get; set; }

    }
}//
