using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    [Authorize]
    public class OrderHistoryController : Controller
    {
        // GET: OrderHistory
        public ActionResult Index()
        {

            var obj = new List<Order>();
            using (var client = new HttpClient())
            {


                if (User.IsInRole("user"))
                {
                    var responseTask = client.GetAsync("https://localhost:44355/api/orderlist?email=" + User.Identity.Name);


                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IEnumerable<Order>>();

                        readTask.Wait();

                        obj = readTask.Result.ToList();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                   // return View(obj);
                }
                else if(User.IsInRole("admin"))
                {
                    var responseTask = client.GetAsync("https://localhost:44355/api/order");


                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IEnumerable<Order>>();

                        readTask.Wait();

                        obj = readTask.Result.ToList();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                   
                }
                else if (User.IsInRole("delivery"))
                {
                    var responseTask = client.GetAsync("https://localhost:44355/api/delivery?email="+User.Identity.Name);


                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IEnumerable<Order>>();

                        readTask.Wait();

                        obj = readTask.Result.ToList();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                    // return View(obj);
                }
                return View(obj);
            }
        }
           
        

        public ActionResult ModalPop(int id)
        {
            var obj = new Order();
            using (var client = new HttpClient())
            {


                var responseTask = client.GetAsync("https://localhost:44355/api/order/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Order>();

                    readTask.Wait();

                    obj = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return PartialView(obj);
        }
    }
    }
