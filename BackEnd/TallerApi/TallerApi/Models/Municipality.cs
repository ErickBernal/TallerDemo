using System.ComponentModel.DataAnnotations;

namespace TallerApi.Models
{
    public class Municipality
    {
        [Key]
        public int IdMunicipality { get; set; }
        public string? Name { get; set; }

        //Fk
        public int DepartmentId { get; set; }

        //Relation
        public virtual Department? Department { get; set; }


    }
}
