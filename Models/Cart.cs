using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class Cart
    {
        [Key]
        public int id { get; set; }
        public string customerId { get; set; }
        public int pizzaId { get; set; }
        public string pizzaName { get; set; }
        public double pizzaPrice { get; set; }
        public string pizzaSize { get; set; }
        public int quantity { get; set; }
    }
}