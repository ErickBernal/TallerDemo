﻿using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taller.Entities
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string? Pwd { get; set; }
    }



    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
    }

    public class Login
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }


    public class Country
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Department>? Departments { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country? Country { get; set; } = null;

        public List<Municipality>? Municipalities { get; set; }
    }

    public class Municipality
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; } = null;

        //public ICollection <Client> Clients { get; set; }
    }
    //-------------------------------------- client------------------------------------------------------
    public class TypeClient
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        //public string Description { get; set; } = string.Empty;
        public ICollection<Client>? Clients { get; set; }
    }

    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int DPI { get; set; }
        public int Nit { get; set; }
        public int Phone { get; set; }
        public int Cellphone { get; set; }

        public int TypeClientId { get; set; }
        public TypeClient? TypeClient { get; set; }


        public int Zone { get; set; }
        public string? Address { get; set; }
        public int MunicipalityId { get; set; }
        public Municipality? Municipality { get; set; }


        //public ICollection<DetalleClientService>? DetalleClientServices { get; set; } //detalle cliente sevicio
        public ICollection<ServiceDetalle>? ServiceDetalles { get; set; } 

    }

}//


