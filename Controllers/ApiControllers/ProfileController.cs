using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Auth.Controllers.ApiControllers
{
    public class ProfileController : ApiController
    {
        private IDBContext context = new ApplicationDbContext();

        public ProfileController()
        {

        }
        public ProfileController(IDBContext iContext)
        {
            context = iContext;
        }
        public IHttpActionResult Get(string email)
        {
            var userDetails = context.Registeration.Where(r => r.Email.ToLower() == email).FirstOrDefault();

            return Ok(userDetails);
        }

        public IHttpActionResult Put([FromBody]ProfileModel profile)
        {
            var res = context.Registeration.Where(r => r.Email.ToLower() == profile.email.ToLower()).FirstOrDefault();
            if(res==null)
            {
                return NotFound();
            }
            res.FirstName = profile.firstName;
            res.LastName = profile.lastName;
            res.Address = profile.address;

            context.SaveChanges();

            return Ok(profile);
        }
    }
}