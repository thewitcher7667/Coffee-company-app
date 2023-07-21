using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Reset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }  
        public User User { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; } 

        public List<ItemsBuyed> ItemsBuyed { get; set; }
    }
}
