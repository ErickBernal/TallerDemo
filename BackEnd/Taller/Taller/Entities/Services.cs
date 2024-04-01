using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taller.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        //public string Description { get; set; } = string.Empty;
        public ICollection<Department> Departments { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection <Municipality> Municipalities { get; set; }
    }

    public class Municipality
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection <Address> Addresses { get; set; }
    }
    //-------------------------------------- client------------------------------------------------------
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int Zone { get; set; }
        public string Description { get; set; }
        public Municipality Municipality { get; set; }
        public ICollection<Client> Clients { get; set; }
    }

    public class TypeClient
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        //public string Description { get; set; } = string.Empty;
        public ICollection<Client> Clients { get; set; }
    }

    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DPI { get; set; }
        public int Nit { get; set; }
        public int Phone { get; set; }
        public int Cellphone { get; set; }
        public TypeClient TypeClient { get; set; }
        public Address Address { get; set; }


        //public ICollection<Department> Departments { get; set; }
    }

    //------------------
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public int Model { get; set; }

        //public ICollection<Department> Departments { get; set; }
    }
    public class VehicleBrand
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }

        //public ICollection<Department> Departments { get; set; }
    }

}//------------------
