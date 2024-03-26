using TallerApi.DataAccess;
using System.Data.Entity;
using TallerApi.Models;

namespace TallerApi.Data.DefaultData
{
    public class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TallerDbContext>();

                if (!context.Country.Any())
                {
                    var countryToAdd = new List<Country>
                    {
                        new Country {IdCountry = 1, Name = "Guatemala"},
                        new Country {IdCountry = 2, Name = "El Salvador"},
                        new Country {IdCountry = 3, Name = "Belice"}
                    };

                    context.Country.AddRange(countryToAdd);
                    context.SaveChanges();
                }

            }


        }

    }
}
