using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ItemsBuyed
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CoffeItem")]
        public virtual int CoffeItemId { get; set; }
        public virtual CoffeItem CoffeItem { get; set; }

        public int Quantity { get; set; }   

        public decimal Price { get; set; }

        [ForeignKey("Reset")]
        public virtual int ResetId { get; set; }
    }
}
