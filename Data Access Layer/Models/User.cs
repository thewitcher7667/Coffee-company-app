using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public override string Id { get; set; }

        public override string UserName { get; set; }

        [EmailAddress]
        [Required]
        public override string Email { get; set; }

        [Required(ErrorMessage ="Password field is requierd")]
        [MinLength(8,ErrorMessage = "'Password' minimum length is 8")]
        [DisplayName("Password")]
        public override string PasswordHash { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("PasswordHash",ErrorMessage = "'ConfirmPassword' and 'Password' do not match.")]
        public string ConfirmPassword { get; set; }

        // Foreign key   
        [ForeignKey("Department")]
        public virtual int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}