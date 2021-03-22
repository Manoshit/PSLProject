using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class PizzaStock
    {
        public int id { get; set; }
        public int pizzaId { get; set; }
        public int quantity { get; set; }
    }

    public class QuantityForm
    {
        public int quantity { get; set; }
    }

    public class StatusForm
    {
        public string status { get; set; }
    }
}