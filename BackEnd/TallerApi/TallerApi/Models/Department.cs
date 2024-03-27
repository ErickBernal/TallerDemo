using System.ComponentModel.DataAnnotations;

namespace TallerApi.Models
{
    public class Department
    {
        [Key]
        public int IdDepartment { get; set; }
        public string? Name { get; set; } 
        
        //Fk
        //public int IdCountry { get; set; }

        //Relation
        //public virtual Country? Country { get; set; }
        //public virtual List<Municipality>? Municipalities { get; set; }
        public virtual Country? IdCountry { get; set; }
        public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();

    }
}
