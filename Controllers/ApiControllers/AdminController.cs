using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
   
    public class AdminController : ApiController
    {
        private IDBContext context = new ApplicationDbContext();

        public AdminController()
        {

        }
        public AdminController(IDBContext iContext)
        {
            context = iContext;
        }
        
        public IHttpActionResult Get()
        {
       
         return Ok(context.Pizzas.ToList());
            
        }

        public IHttpActionResult Get(int id)
        {
            using (context)
            {

                var res = context.Pizzas.FirstOrDefault(p => p.id == id);

                if (res == null)
                {
                    return Content(HttpStatusCode.NotFound, "Pizza not found");
                }
                else
                    return Ok(res);
            }
        }
        [Authorize]
        public IHttpActionResult Post([FromBody] Pizza pizza)
        {
            using (context)
            {
                context.Pizzas.Add(pizza);
                context.SaveChanges();
                return Ok(pizza);
            }

        }
        [HttpPut]
        public IHttpActionResult Put(int id,[FromBody] Pizza pizza)
        {
            using (context)
            {
                try
                {
                    if (id != pizza.id)
                    {
                        return BadRequest();
                    }

                    context.MarkAsModified(pizza); 
                    context.SaveChanges();
                    return Ok(pizza);
                    

                }
                catch (System.Reflection.TargetException  e)
                {
                    Console.WriteLine(e);
                    return Content(HttpStatusCode.NotFound, "Pizza not found");
                }
                    
            }
      
        }

        public IHttpActionResult Delete(int id)
        {
            using (context)
            {

                var res = context.Pizzas.Find(id);

                if (res == null)
                {
                    return Content(HttpStatusCode.NotFound, "Pizza not found");
                }
                else
                {
                    context.Pizzas.Remove(res);
                    context.SaveChanges();
                    return Ok(res);

                }
            }
        }


    }
}
