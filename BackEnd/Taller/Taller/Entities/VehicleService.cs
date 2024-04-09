using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taller.Entities
{

    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public int Model { get; set; }

        public ICollection<VehicleLinea>? vehicleLineas { get; set; }

    }
    public class VehicleBrand
    {
        [Key]
        public int Id { get; set; }
        public string? Brand { get; set; } 

        public ICollection<VehicleLinea>? vehicleLineas { get; set; }
    }

    public class VehicleLinea
    {
        [Key]
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Type { get; set; }
        public string? Line { get; set; }

        [ForeignKey("VehicleModel")]

        public int VehicleModelId { get; set; }
        public VehicleModel? VehicleModel { get; set; } 

        [ForeignKey("VehicleBrand")]

        public int VehicleBrandId { get; set; }
        public VehicleBrand? VehicleBrand { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; }
        public ICollection<VehiclePartsCompatible>? vehiclePartsCompatibles { get; set; }

    }

    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public string? Placa { get; set; }

        [ForeignKey("VehicleLinea")]
        public int VehicleLineaId { get; set; }
        public VehicleLinea? VehicleLinea { get; set; } 

        public ICollection<ServiceDetalle>? ServiceDetalles { get; set; } 

    }

    public class VehiclePart//repuesto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public double UnitPrice { get; set; }
        public ICollection<VehiclePartsCompatible>? VehiclePartsCompatibles { get; set; }
        public ICollection<DetalleVehicleParts>? DetalleVehicleParts { get; set; } //detalle repuesto


    }

    public class VehiclePartsCompatible
    {
        [Key]
        public int Id { get; set; }
        public int VehicleLineaId { get; set; }
        public VehicleLinea? VehicleLinea { get; set; }
        public int VehiclePartId { get; set; }
        public VehiclePart? VehiclePart { get; set; }
    }

}
