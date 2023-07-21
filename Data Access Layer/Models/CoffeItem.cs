using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class CoffeItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
