using System.ComponentModel.DataAnnotations;

namespace TallerApi.Models
{
    public class Municipality
    {
        [Key]
        public int IdMunicipality { get; set; }
        public string? Name { get; set; }

        //Fk
        //Relation
        public virtual Department? IdDepartment { get; set; }


    }
}
