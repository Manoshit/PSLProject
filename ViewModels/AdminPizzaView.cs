using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.ViewModels
{
    public class AdminPizzaView
    {
        public List<Pizza> pizzas{get;set;}
        public List<Size> sizes { get; set; }

        public List<PizzaStock> pizzaStocks { get; set; }
    }
}