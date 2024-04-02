using System.ComponentModel.DataAnnotations;

namespace Taller.Entities
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<ServiceListWork> ServiceListWorks { get; set; }
        public ICollection<ServiceDetalle> ServiceDetalles { get; set; }

    }


    public class ServiceWork
    {
        [Key]
        public int Id { get; set; }
        public string Work { get; set; }
        public string PriceWork { get; set; }
        public ICollection<ServiceListWork> ServiceListWorks { get; set; }
        public ICollection<DetalleWorkService> DetalleWorkServices { get; set; }

    }
    public class ServiceListWork
    {
        [Key]
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int ServiceWorkId { get; set; }
        public ServiceWork ServiceWork { get; set; }
    }

    public class ServiceDetalle
    {
        [Key]
        public int Id { get; set; }
        public int State { get; set; }
        public int Km { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<DetalleClientService> DetalleClientServices { get; set; }
        public ICollection<DetalleWorkService> DetalleWorkServices { get; set; }
        public ICollection<DetalleVehicleParts> DetalleVehicleParts { get; set; }

    }
}//
