using Newtonsoft.Json;
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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Create()
        {
            return View();
        }
       
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
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/inventory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<PizzaStock>>();
                    readTask.Wait();

                    obj.pizzaStocks = readTask.Result.ToList();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(obj);

        }

        public ActionResult Edit(int id)
        {
            var obj = new AdminEditPizzaView();
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/admin" + "/"+id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Pizza>();
                    readTask.Wait();
                    obj.pizza = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/size"+"/"+id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Size>>();
                    readTask.Wait();

                    obj.size = readTask.Result.ToList();
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