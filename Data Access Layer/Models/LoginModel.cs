using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }    
    }
}
