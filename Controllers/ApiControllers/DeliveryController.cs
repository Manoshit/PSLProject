using Auth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Auth.Controllers.ApiControllers
{
    public class DeliveryController : ApiController
    {
        private IDBContext db = new ApplicationDbContext();

        public DeliveryController()
        {

        }
        public DeliveryController(IDBContext icontext)
        {
            db = icontext;
        }


        public IHttpActionResult Get()
        {
            var res = db.Registeration.Where(r => r.UserType == "Delivery").ToList();
            return Ok(res);
        }

        public IHttpActionResult Get(string email)
        {
            var res = db.Orders.Where(o=> o.DeliveryExecutive.Email.ToLower() == email.ToLower()).ToList();
            return Ok(res.OrderByDescending(o=>o.timeStamp));
        }

        // POST api/<controller>
        public IHttpActionResult Post(Models.Registeration user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        var crypto = new SimpleCrypto.PBKDF2();
                        var encrypPass = crypto.Compute(user.Password);
                        var newUser = db.Registeration.Create();
                        newUser.Email = user.Email;
                        newUser.Password = encrypPass;
                        newUser.PasswordSalt = crypto.Salt;
                        newUser.FirstName = user.FirstName;
                        newUser.LastName = user.LastName;
                        newUser.UserType = "Delivery";
                        newUser.CreatedDate = DateTime.Now;
                        newUser.IsActive = true;
                        newUser.IpAddress = "642 White Hague Avenue";
                        newUser.Address = user.Address;
                        db.Registeration.Add(newUser);
                        db.SaveChanges();
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Check Details");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return Ok();
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var res = db.Registeration.Find(id);
            if(res.IsActive)
            {
                db.Registeration.Remove(res);
            }
            else
            {
                return BadRequest("Delivery executive is currently in transit");
            }
            db.SaveChanges();
            return Ok(res);
        }
    }
}