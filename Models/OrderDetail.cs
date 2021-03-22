using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
        public int quantity { get; set; }
        public string pizzaSize { get; set; }

        public double price { get; set; }
    }
}