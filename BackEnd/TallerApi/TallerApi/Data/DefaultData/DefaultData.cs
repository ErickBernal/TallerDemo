using CsvHelper;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using TallerApi.DataAccess;
using TallerApi.Models;


namespace TallerApi.Data.DefaultData
{
    public class DefaultData
    {

        private readonly TallerDbContext _context;

        public DefaultData(TallerDbContext context)
        {
            _context = context;
        }


        static string currentDirectory = Directory.GetCurrentDirectory();
        static string countryPath = Path.Combine(currentDirectory, "Data\\DefaultData\\country.csv");
        static string departmentPath = Path.Combine(currentDirectory, "Data\\DefaultData\\department.csv");


        //public  void SetDefaultData()
        //{
        //    var country = new Country { IdCountry = 1, Name = "Guatemala" };
        //    var department = new Department { IdDepartment = 1, CountryId = 1, Name = "Alta Verapaz" };
        //    var muni = new Municipality { IdMunicipality = 1, Name = "San Juan Chamelco", DepartmentId = 1 };

        //    department.Municipalities = new List<Municipality> { muni };
        //    country.Departments = new List<Department> {department };
        //    _context.Country.Add(country);
        //    _context.SaveChanges();
        //}


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

        //public static List<Country> GetDepartmentsToLoad()
        //{
        //    List<Department> list = new List<Department> {
        //        new Department { IdDepartment = 1, CountryId = 1, Name = "Alta Verapaz" },
        //        new Department { IdDepartment = 2, CountryId = 1, Name = "Baja Verapaz" },
        //        new Department { IdDepartment = 3, CountryId = 1, Name = "Chimaltenango"}
        //    };
        //    return new List<Country> {  };
        //}





    }//class
}//namespace
