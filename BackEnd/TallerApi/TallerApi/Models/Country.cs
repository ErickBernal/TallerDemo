using System.ComponentModel.DataAnnotations;

namespace TallerApi.Models
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Department> Departments { get; set;} = new List<Department>();

    }
}
