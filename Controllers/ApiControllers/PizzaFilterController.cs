using Auth.Models;
using Auth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
    public class PizzaFilterController : ApiController
    {
      
        public IHttpActionResult Get( string id)
        {
            var context = new ApplicationDbContext();

            AdminPizzaView obj = new AdminPizzaView();

            obj.pizzas = context.Pizzas.Where(p => p.category.ToLower() == id.ToLower()).ToList();

            obj.sizes = new List<Size>();
            obj.pizzaStocks = new List<PizzaStock>();

            foreach (var i in obj.pizzas)
            {
                var sizeObject = context.Sizes.Where(s => s.pizzaId == i.id).ToList();
                var inventoryObject = context.PizzaStocks.Where(s => s.pizzaId == i.id).ToList();
                obj.sizes.AddRange(sizeObject);
                obj.pizzaStocks.AddRange(inventoryObject);
            }

            return Ok(obj);
        }

      
   }
}