using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class Size
    {
        [Key]
        public int id { get; set; }
       
        public int? pizzaId { get; set; }

        public string name { get; set; }

        public double price { get; set; }
    }
}