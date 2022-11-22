using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskAuthenticationAuthorization.Models.Auth.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(50)]
        public string Email { get; set; }

        [PasswordPropertyText]
        [Required, MaxLength(16)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }
        
        public Customer Customer { get; set; }
        
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}