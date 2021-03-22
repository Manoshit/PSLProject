using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.ViewModels
{
    public class InventoryView
    {
        public List<Pizza> pizzas { get; set; }
        public List<PizzaStock> quantities { get; set; }

    }
}