using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskAuthenticationAuthorization.Models.Auth;
using TaskAuthenticationAuthorization.Models.Auth.Entities;

namespace TaskAuthenticationAuthorization.Models
{
    public enum Discount
    {
        O, R, V
    }
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required, MaxLength(80)]
        public string Address { get; set; }
        
        public Discount? Discount { get; set; }
        public ICollection<Order> Orders { get; set; }  
        
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
