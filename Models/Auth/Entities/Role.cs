using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskAuthenticationAuthorization.Models.Auth.Entities;

namespace TaskAuthenticationAuthorization.Models.Auth
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public virtual List<User> Users { get; set; } = new List<User>();
    }
}