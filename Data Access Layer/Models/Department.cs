using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 
    }
}
