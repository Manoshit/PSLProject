using Auth.Models;
using Auth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    [Authorize(Roles =("user"))]
    public class ClientController : Controller
    {
     
        public ActionResult Index()
        {
            var obj = new AdminPizzaView();
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/admin");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Pizza>>();
                    readTask.Wait();

                    obj.pizzas = readTask.Result.ToList();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/size");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Size>>();
                    readTask.Wait();

                    obj.sizes = readTask.Result.ToList();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(obj);
        }

        public ActionResult FindPizza(string id)

        {
            var obj = new AdminPizzaView();
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/pizzafilter" + "/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AdminPizzaView>();
                    readTask.Wait();

                    obj = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View("Index",obj);
        }


    }
}