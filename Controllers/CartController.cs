using Auth.Models;
using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    [Authorize(Roles ="user")]
 
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var obj = new List<Cart>();
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/cart" +"/"+ User.Identity.Name.Substring(0, User.Identity.Name.Length - 4));
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Cart>>();

                    readTask.Wait();

                    obj = readTask.Result.ToList();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(obj);
        }
    }
}