﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAuthenticationAuthorization.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public float Quantity { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}