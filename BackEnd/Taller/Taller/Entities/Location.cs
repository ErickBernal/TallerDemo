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
    }

}
