using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class OrderForm
    {
        public int id { get; set; }
        [Required]
        public string customerId { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public double totalAmount { get; set; }
        public string feedback { get; set; }
        public string timeStamp { get; set; }
    }
}