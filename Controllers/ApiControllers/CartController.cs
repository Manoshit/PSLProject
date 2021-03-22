using Auth.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
    public class CartController : ApiController
    {
        private IDBContext context = new ApplicationDbContext();

        public CartController()
        {

        }
        public CartController(IDBContext iContext)
        {
            context = iContext;
        }

        public IHttpActionResult Get(string id)
        {
            List<Cart> res = new List<Cart>();
            using (context)
            {

                 res = context.Carts.Where(c => c.customerId.Contains(id)).ToList();

                if (res == null)
                {
                    return Content(HttpStatusCode.NotFound, "Cart not found");
                }
                else
                    return Ok(res);
            }
        }

        public IHttpActionResult Post([FromBody] Cart cart)
        {
            using (context)
            {

                // Check if the pizza already exists in the cart
                var res = from c in context.Carts.ToList()
                          where (c.customerId == cart.customerId) && (c.pizzaId == cart.pizzaId) && (c.pizzaSize == cart.pizzaSize)
                          select c;

                if (res.Count() > 0)
                {
                    // Item already exists in the cart
                    return BadRequest("Pizza already added in the cart!");
                }


                context.Carts.Add(cart);
                context.SaveChanges();
                var json = JsonConvert.SerializeObject(cart);
                return Ok(json);
            }

        }
        [HttpPut]
        public IHttpActionResult Put([FromUri]int id, int quantity)
        {
            using (context)
            {
                try
                {
                    // Get cart item
                    var cart = context.Carts.Find(id);
                    cart.quantity = (int)quantity;

                  //  context.MarkAsModified(cart);
                    context.SaveChanges();
                    return Ok(cart);


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Content(HttpStatusCode.NotFound, e.Message);
                }

            }

        }

        public IHttpActionResult Delete(int id)
        {
            using (context)
            {

                var res = context.Carts.Find(id);

                if (res == null)
                {
                    return Content(HttpStatusCode.NotFound, "Cart not found");
                }
                else
                {
                    context.Carts.Remove(res);
                    context.SaveChanges();
                    return Ok(res);

                }
            }
        }
    }
}