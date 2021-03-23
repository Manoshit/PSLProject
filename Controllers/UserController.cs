using Auth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Auth.Controllers
{
    public class UserController : Controller
    {
          public ActionResult Index()
          {
                return View("Index");
          }


            [AllowAnonymous]
            [HttpGet]
            public ActionResult LogIn()
            {
                return View("LogIn");
            }

       

        [HttpPost]
            public ActionResult LogIn(Models.Registeration userr)
            {
                
                if (IsValid(userr.Email, userr.Password))
                {
                   FormsAuthentication.SetAuthCookie(userr.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login details are wrong.");
                }
                return View(userr);
            }
            [AllowAnonymous]
            [HttpGet]
            public ActionResult Register()
            {
                return View("Register");
            }



            [HttpPost]
            public ActionResult Register(Models.Registeration user)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                       
                        using (var db = new ApplicationDbContext())
                        {
                        var res = db.Registeration.Where(r => r.Email.Trim().ToLower() == user.Email.Trim().ToLower()).FirstOrDefault();

                        if (res == null)
                        {
                            var crypto = new SimpleCrypto.PBKDF2();
                            var encrypPass = crypto.Compute(user.Password);
                            var newUser = db.Registeration.Create();
                            newUser.Email = user.Email.ToLower();
                            newUser.Password = encrypPass;
                            newUser.PasswordSalt = crypto.Salt;
                            newUser.FirstName = user.FirstName;
                            newUser.LastName = user.LastName;
                            newUser.UserType = "User";
                            newUser.CreatedDate = DateTime.Now;
                            newUser.IsActive = true;
                            newUser.ContactNumber = user.ContactNumber;
                            newUser.Address = user.Address;
                            db.Registeration.Add(newUser);
                            db.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "User already exists");
                        }
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
                return View();
            }



            public ActionResult LogOut()
            {
          
               FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            public bool NameValidation(string fname, string lname)
            {
                if (fname == null || lname == null)
                {
                    return false;
                }
                if (Regex.IsMatch(fname, "^[a-zA-Z]+$") || Regex.IsMatch(lname, "^[a-zA-Z]+$")) //check this only alpha value
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }



            public bool EmailValidation(string email)
            {
                if (email != null)
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(email);
                    if (match.Success)
                        return true;
                    else
                        return false;



                }
                else
                {
                    return false;
                }
            }



            public bool PasswordValidation(string pass)
            {
                if (pass != null)
                {
                    if (pass.Length < 6)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }



                }
                else
                {
                    return false;
                }
            }
            public bool IsValid(string email, string password)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                bool IsValid = false;



                using (var db = new ApplicationDbContext())
                {
                    var user = db.Registeration.Where(u => u.Email == email).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Password == crypto.Compute(password, user.PasswordSalt))
                        {
                            IsValid = true;
                        }
                    }
                }
                return IsValid;
            }
        }
    }
