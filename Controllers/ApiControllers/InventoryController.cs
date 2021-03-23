using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
    public class Quantity
    {
        public int value { get; set; }
    }
    public class InventoryController : ApiController
    {
       private IDBContext context = new ApplicationDbContext();

        public InventoryController()
        {

        }
        public InventoryController(IDBContext icontext)
        {
            context = icontext;
        }

        public IHttpActionResult Get()
        {
            return Ok(context.PizzaStocks.ToList());
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] PizzaStock stock)
        {
            context.PizzaStocks.Add(stock);
            context.SaveChanges();
            return Ok(stock);
        }

        public IHttpActionResult Put(int id, [FromBody] Quantity quantity)
        {
            var res = context.PizzaStocks.Where((ps)=>ps.pizzaId==id).FirstOrDefault();
            if(res==null)
            {
                return BadRequest();
            }
            res.quantity = quantity.value;
            context.SaveChanges();

            return Ok(res);
        }

        public IHttpActionResult Delete (int id)
        {
            var res = context.PizzaStocks.Where((ps) => ps.pizzaId == id).FirstOrDefault();
            if (res == null)
            {
                return NotFound();
            }
            context.PizzaStocks.Remove(res);
            context.SaveChanges();
            return Ok(res);
        }

    }
}