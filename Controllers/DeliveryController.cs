using Auth.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    [Authorize(Roles =("admin"))]
    public class DeliveryController : Controller
    {
        // GET: Delivery
        public ActionResult Index()
        {
            var obj = new List<Registeration>();
            using (var client = new HttpClient())
            {
                

                var responseTask = client.GetAsync("https://localhost:44355/api/delivery");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Registeration>>();
                   
                    readTask.Wait();
                     obj = readTask.Result.Where(o=>o.IsRemoved!=true).ToList();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(obj);
        }

        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Registeration deliveryExec)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(deliveryExec);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var responseTask = client.PostAsync("https://localhost:44355/api/delivery",data) ;
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    readTask.Wait();

                   
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return RedirectToAction("Index", "Delivery");
        }

        public ActionResult Edit(int id)
        {
            var obj = new Registeration();
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/delivery");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Registeration>>();

                    readTask.Wait();
                    obj = readTask.Result.Where(o => o.UserId == id).FirstOrDefault();

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