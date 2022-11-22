using System.ComponentModel.DataAnnotations;

namespace TaskAuthenticationAuthorization.Models.Auth.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required, MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmedPassword { get; set; }
        
        public Customer Customer { get; set; }
    }
}