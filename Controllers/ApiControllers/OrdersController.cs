using Auth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Auth.Controllers.ApiControllers
{
    public class OrderController : ApiController
    {
        private IDBContext db = new ApplicationDbContext();

    
        public IHttpActionResult GetOrders()
        {
           
            return Ok(db.Orders.OrderByDescending(o=>o.timeStamp).ToList());
        }

        
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
         
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
      [Route("api/orderlist")]
        public IHttpActionResult GetOrderByEmail(string email)
        {
            var emailId = db.Registeration.Where(r => r.Email.ToLower() == email.ToLower()).FirstOrDefault();
            var orderQuery = from o in db.Orders.ToList()
                             where o.customer.UserId == emailId.UserId
                             select o;

            if (orderQuery.Count() == 0)
            {
                return NotFound();
            }

            var orders = orderQuery.OrderByDescending(o=>o.timeStamp).ToList();

            return Ok(orders);
        }

        public IHttpActionResult PutOrder(int id, StatusForm statusForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!OrderExists(id))
            {
                return NotFound();
            }

            // Get the order from db
            Order order = db.Orders.Find(id);

            if (statusForm.status == "Cancelled" || statusForm.status=="Delivered" )
            {
                // Set delivery exec of the order as active
                order.DeliveryExecutive.IsActive = true;
            }
            var orderItems = order.OrderDetails.ToList();

            foreach (var item in orderItems)
            {
                var pizzaStockItem = (from p in db.PizzaStocks.ToList()
                                      where p.pizzaId == item.Pizza.id
                                      select p).First();

                pizzaStockItem.quantity += item.quantity;

                db.MarkAsModified(pizzaStockItem);
            }
        
        order.status = statusForm.status;

           // db.MarkAsModified(order);

            db.SaveChanges();


            return Ok(order);
        }



        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(OrderForm orderForm)
        {
            var emailId = db.Registeration.Where(r => r.Email.ToLower() == orderForm.customerId.ToLower()).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cartItems = (from c in db.Carts.ToList()
                             where c.customerId == orderForm.customerId
                             select c).ToList();

            foreach (var item in cartItems)
            {
                // Check available quantity of pizza
                int availableQuantity = db.PizzaStocks.Where((ps) => ps.pizzaId == item.pizzaId).Select(p => p.quantity).First();

                if (availableQuantity < item.quantity)
                {
                    return BadRequest($"Sufficient quantity of {item.pizzaName} pizza is not available!");
                }

            }


            // Check delivery executive availability
            var deliveryExecs = from d in db.Registeration.ToList()
                                where (d.UserType == "Delivery") && (d.IsActive == true)
                                select d;

            if (deliveryExecs.Count() == 0)
            {
                return BadRequest("No Delivery Executive available for delivery!");
            }

            // Get delivery executive
            var delExec = deliveryExecs.First();
            delExec.IsActive = false;







            // Create Order object
            Order order = new Order
            {
               
                status = orderForm.status,
                totalAmount = orderForm.totalAmount,
                timeStamp = orderForm.timeStamp,
                DeliveryExecutive = delExec
            };

            order.customer = emailId;

            // Add to db
            db.Orders.Add(order);

            // Get pizzas added to the cart for this customer
            var pizzaIds = (from c in db.Carts.ToList()
                            where c.customerId == orderForm.customerId
                            select c).ToList();

            // Create OrderDetail objects
            foreach (var pid in pizzaIds)
            {
                // Get Pizza using pizzaId
                Pizza pizza = db.Pizzas.Find(pid.pizzaId);

                // Set order and pizza of orderDetail
                OrderDetail orderDetail = new OrderDetail
                {
                    Order = order,
                    Pizza = pizza,
                    quantity = pid.quantity,
                    pizzaSize = pid.pizzaSize,
                    price=pid.pizzaPrice,
                };

                var pizzaStockItem = (from p in db.PizzaStocks.ToList()
                                      where p.pizzaId == pid.pizzaId
                                      select p).First();

                pizzaStockItem.quantity -= pid.quantity;

                db.MarkAsModified(pizzaStockItem);

                db.OrderDetails.Add(orderDetail);
            }

            // Remove cart items
            var carts = (from c in db.Carts.ToList()
                         where c.customerId == orderForm.customerId
                         select c).ToList();

            foreach (var item in carts)
            {
                db.Carts.Remove(item);
            }

            // Commit changes to db             
            db.SaveChanges();
      return CreatedAtRoute("DefaultApi", new { id = order.id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDetailsRows = (from row in db.OrderDetails.ToList()
                                    where row.Order == order
                                    select row).ToList();


            foreach (var item in orderDetailsRows)
            {
                db.OrderDetails.Remove(item);
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.id == id) > 0;
        }
    }

}