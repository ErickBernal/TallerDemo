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

        //public static List<Department> GetDepartmentsToLoad()
        //{
        //    List<Department> list = new List<Department> {
        //        new Department { Id = 1, CountryId = 1, Name = "Alta Verapaz" },
        //        new Department { Id = 2, CountryId = 1, Name = "Baja Verapaz" },
        //        new Department { Id = 3, CountryId = 1, Name = "Chimaltenango"}
        //    };

        //    return list;
        //}


    }//class
}//namespace
