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
    [Authorize(Roles =("admin"))]
    public class InventoryController : Controller
    {

        public ActionResult Index()
        {
            var obj = new InventoryView();
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


                var responseTask = client.GetAsync("https://localhost:44355/api/inventory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<PizzaStock>>();
                    readTask.Wait();

                    obj.quantities = readTask.Result.ToList();
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