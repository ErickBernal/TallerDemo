using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Text;
using Taller.Data;
using Taller.Entities;


namespace TallerApi.Data.DefaultData
{
    public class DbInitializer
    {

        private readonly DataContext _context;

        public DbInitializer(DataContext context)
        {
            _context = context;
        }

        static string currentDirectory = Directory.GetCurrentDirectory();
        static string countryPath = Path.Combine(currentDirectory, "Data\\DefaultData\\country.csv");
        static string departmentPath = Path.Combine(currentDirectory, "Data\\DefaultData\\department.csv");
        static string municipalityPath = Path.Combine(currentDirectory, "Data\\DefaultData\\muni.csv");

        public static List<Country> GetCountriesToLoad()
        {
            List<Country> list = new List<Country>();
            using (var st = new StreamReader(countryPath))
            using (var csvReader = new CsvReader(st, CultureInfo.InvariantCulture))
            {
                while (csvReader.Read())
                {
                    var redcord = csvReader.GetRecord<Country>();
                    //Console.WriteLine($"IdCountry: {redcord.IdCountry}, Name: {redcord.Name}");
                    list.Add(redcord);
                }
            }
            return list;
        }
        public static List<Department> GetDepartmentsToLoad()
        {
            List<Department> list = new List<Department>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
                Delimiter = "," // The delimiter is a comma.
            };
            using (var fs = File.Open(departmentPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, Encoding.UTF8))
                using (var csv = new CsvReader(textReader, configuration))
                {
                    var data = csv.GetRecords<Department>();

                    foreach (var record in data)
                    {
                        list.Add(new Department { Id = record.Id, CountryId = record.CountryId, Name = record.Name });
                    }
                }
            }
            return list;
        }
        public static List<Municipality> GetMunicipalitiesToLoad()
        {
            List<Municipality> list = new List<Municipality>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.UTF8, // Our file uses UTF-8 encoding.
                Delimiter = "," // The delimiter is a comma.
            };
            using (var fs = File.Open(municipalityPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var textReader = new StreamReader(fs, Encoding.UTF8))
                using (var csv = new CsvReader(textReader, configuration))
                {
                    var data = csv.GetRecords<Municipality>();

                    foreach (var record in data)
                    {
                        list.Add(new Municipality { Id = record.Id, DepartmentId = record.DepartmentId, Name = record.Name });
                    }
                }
            }
            return list;
        }

        public static List<Country> Prub()
        {
            List<Municipality> l_muni = GetMunicipalitiesToLoad();
            List<Department> l_dep = GetDepartmentsToLoad();
            List<Country> l_country = GetCountriesToLoad();

            foreach (Country c in l_country)
            {
                c.Departments =[];
                foreach (Department d in l_dep)
                {
                    if (d.CountryId == c.Id)
                    {
                        d.CountryId = c.Id;
                        d.Municipalities = []; // Mover esta línea aquí para evitar que se cree una nueva lista en cada iteración del bucle externo
                        d.Country = c; // Establecer el país del departamento
                        foreach (Municipality m in l_muni)
                        {
                            if (m.DepartmentId == d.Id)
                            {
                                m.DepartmentId = d.Id;
                                m.Department = d; // Establecer el departamento de la municipalidad
                                d.Municipalities.Add(m);
                            }
                        }
                        c.Departments.Add(d);
                    }
                }
            
            
            }

            List<Municipality> listm = new List<Municipality>();
            listm.Add(new Municipality { Id = 3, DepartmentId = 2, Name = "municipio"});


            List<Department> listd = new List<Department>();
            listd.Add(new Department { Id = 2, CountryId = 1, Name = "Departamaento" ,Municipalities= listm});


            List<Country> list = new List<Country>();
            list.Add(new Country { Id = 1, Name = "country", Departments = listd });



            return list;
        }


        static List<TypeClient> typeClients = new List<TypeClient> {
                new TypeClient { Id = 1,Type = "Individual" },
                new TypeClient { Id = 2,Type = "Empresa" }
            };

        public static List<TypeClient> GetTypeClientToLoad()
        {
            return typeClients;
        }

        public static List<VehiclePart> GetVehiclePartsToLoad()
        {
            return new List<VehiclePart> {
                new VehiclePart { Id = 1,Name = "Aceite 10 w 40",Stock = 10, UnitPrice = 100 },
                new VehiclePart { Id = 2,Name = "Aceite 20 w 50",Stock = 9, UnitPrice = 99 },
                new VehiclePart { Id = 3,Name = "Llanta Rin 22",Stock = 12, UnitPrice = 400 },
                new VehiclePart { Id = 4,Name = "Llanta Rin 17",Stock = 4, UnitPrice = 250 },
                new VehiclePart { Id = 5,Name = "Plumilla",Stock = 25, UnitPrice = 50 },
                new VehiclePart { Id = 6,Name = "Pastillas de freno",Stock = 10, UnitPrice = 200 }
            };
        }
        public static List<VehicleBrand> GetVehicleBrandToLoad()
        {
            return new List<VehicleBrand>
            {
                new VehicleBrand {Id = 1, Brand = "Toyota" },
                new VehicleBrand {Id = 2, Brand = "Susuki" } ,
                new VehicleBrand {Id = 3, Brand = "Kia" },
                new VehicleBrand {Id = 4, Brand = "Subaru" }
            };
        }
        public static List<VehicleModel> GetVehicleModelToLoad()
        {
            return new List<VehicleModel>
            {
                new VehicleModel {Id= 1, Model= 2000 },
                new () {Id= 2, Model= 2001},
                new () {Id= 3, Model= 2002},
                new () {Id= 4, Model= 2003},
                new () {Id= 5, Model= 2004},
                new () {Id= 6, Model= 2006},
                new () {Id= 7, Model= 2011},
                new () {Id= 8, Model= 2020}
            };
        }
        public static List<VehicleLinea> GetVehicleLineasToLoad()
        {

            return new List<VehicleLinea>
            {
                new VehicleLinea { Id = 1, Color = "Negro", Type = "pick up", Line = "Tacoma" ,VehicleModelId = 1, VehicleBrandId = 1},
                new VehicleLinea { Id = 2, Color = "Azul", Type = "cedan", Line = "Yaris" ,VehicleModelId = 1, VehicleBrandId = 1},
                new VehicleLinea { Id = 3, Color = "verde", Type = "pick up", Line = "Carry" , VehicleModelId = 6,VehicleBrandId = 2}
            };
        }

        public static List<ServiceType> GetServiceTypeToLoad()
        {
            return new List<ServiceType>
            {
                new ServiceType {Id = 1,Type = "Individual"},
                new ServiceType {Id = 2,Type = "Empresa"},

            };
        }
        public static List<ServiceWork> GetServiceWorkToLoad()
        {
            return new List<ServiceWork>
            {
                new ServiceWork { Id = 1, Work = "Servicio Menor", PriceWork=300},
                new ServiceWork { Id = 2, Work = "Servicio Recurrente", PriceWork=400},
                new ServiceWork { Id = 3, Work = "Servicio Mayor", PriceWork=500},
                new ServiceWork { Id = 4, Work = "Servicio por flotilla", PriceWork=1000},
            };
        }
    }//class
}//namespace
