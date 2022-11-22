using System.ComponentModel.DataAnnotations;

namespace TaskAuthenticationAuthorization.Models.Auth.ViewModels
{
    public class LogInViewModel
    {
        [Required, MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required, MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}