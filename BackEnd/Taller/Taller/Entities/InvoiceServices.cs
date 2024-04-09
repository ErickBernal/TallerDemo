
using System.ComponentModel.DataAnnotations;

namespace Taller.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
        public ICollection<DetalleClientService>? DetalleClientServices { get; set; }
        public ICollection<DetalleWorkService>? DetalleWorkServices { get; set; }
        public ICollection<DetalleVehicleParts>? DetalleVehiclePartss { get; set; }
    }
    public class DetalleClientService
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int ServiceDetalleId { get; set; }
        public ServiceDetalle? ServiceDetalle { get; set; }
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }


    public class DetalleWorkService
    {
        [Key]
        public int Id { get; set; }

        public int ServiceWorkId { get; set; }
        public ServiceWork? ServiceWork { get; set; }
        public int ServiceDetalleId { get; set; }
        public ServiceDetalle? ServiceDetalle { get; set; }
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }

    public class DetalleVehicleParts
    {
        [Key]
        public int Id { get; set; }

        public int VehiclePartId { get; set; }
        public VehiclePart? VehiclePart { get; set; }
        public int ServiceDetalleId { get; set; }
        public ServiceDetalle? ServiceDetalle { get; set; }
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}//
