using System.ComponentModel.DataAnnotations;

namespace TallerApi.Models
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }
        public string? Name { get; set; }

        public virtual List<Department>? Departments { get; set;}
    }
}
