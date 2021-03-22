using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
   
    public class SizeController : ApiController
    {
        private IDBContext context = new ApplicationDbContext();

        public SizeController()
        {

        }
        public SizeController(IDBContext IContext)
        {
            context = IContext;
        }
        public IHttpActionResult Get()
        {
           
            return Ok(context.Sizes.ToList());
        }



        public IHttpActionResult Get(int id)
        {
            using (context)
            {
                var result = context.Sizes.Where(s => s.pizzaId == id).ToList();

                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "Size not found");
                }
                else
                {
                    return Ok(result.ToList());
                }
            }
        }
       
        public IHttpActionResult Post([FromBody] Size s)
        {
            using (context)
            {
               

                context.Sizes.Add(s);
                context.SaveChanges();

                return Ok(s);
            }
        }

        
        public IHttpActionResult Put(int id,[FromBody] Size s)
        {
            using (context)
            {

                var result = context.Sizes.Where(size => size.pizzaId == id).ToList();

                foreach (var sizeItem in result)
                {
                    if (s.id == sizeItem.id)
                    {
                        sizeItem.price = s.price;

                    }
                }

                context.SaveChanges();
                return Ok(s);
            }
            
        }


        [HttpDelete]
        [Route("api/size/DeleteSingle/{id}")]
        public IHttpActionResult DeleteSingle (int id)
        {
            using (context)
            {
                var res = context.Sizes.Find(id);
                context.Sizes.Remove(res);
                context.SaveChanges();

                return Ok(res);
            }
        }


       [HttpDelete]
        public IHttpActionResult Delete([FromUri]int id)
        {
            using (context)
            { 
                var res = context.Sizes.Where(s=>s.pizzaId==id).ToList();

                if (res==null)
                {
                    return Content(HttpStatusCode.NotFound, "Size not found");
                }

                else
                {
                    foreach(var item in res)
                    {
                        var temp = context.Sizes.Find(item.id);
                        context.Sizes.Remove(temp);
                    }
                 
                    context.SaveChanges();
                    return Ok(res);
                }

            }
        }
    }
}