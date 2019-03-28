using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace LoginRegistration.Models
{

    namespace LoginRegistration.Controllers
    {

        public class HomeController : Controller
        {
            private MyContext dbContext;

            public HomeController(MyContext context)
            {
                dbContext = context;
            }

            [Route("")]
            [HttpGet]
            public IActionResult Index()
            {
                List<Register> AllUsers = dbContext.Users.ToList();
                return View();
            }
            [Route("Login")]
            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }


            [Route("Create")]
            [HttpPost]

            public IActionResult Create(Register newUser)
            {

                if (ModelState.IsValid)
                {
                    if (dbContext.Users.Any(u => u.Email == newUser.Email))
                    {
                        ModelState.AddModelError("Email", "Email already in use!");
                        return View("Index");
                    }
                   
                    Register person = new Register()
                    {
                        FirstName = Request.Form["FirstName"],
                        LastName = Request.Form["LastName"],
                        Email = Request.Form["Email"],
                        Password = Request.Form["Password"]

                    };
                    System.Console.WriteLine(newUser.Email);
                    HttpContext.Session.SetString("Email", newUser.Email);
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();

                    return RedirectToAction("Success");

                }
                else
                {
                    return View("Index");
                }

            }
            [Route("Enter")]
            [HttpPost]
            public IActionResult Enter(Login newUser)
            {
                if (ModelState.IsValid)
                {
                    var userInDb =  dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
                    if(userInDb==null)
                    {
                        ModelState.AddModelError("Email", "Invalid Email or Password");
                        return View("Login");
                    }
                    var hasher = new PasswordHasher<Login>();
                    var result = hasher.VerifyHashedPassword(newUser, userInDb.Password,newUser.Password);

                    if (result == 0)
                    {
                        ModelState.AddModelError("Password", "Incorrect!");
                        return View("Login");
                    }
                    
                    Login person = new Login()
                    {
                        Email = Request.Form["Email"],
                        Password = Request.Form["Password"]
                    };
                    // VIEWBAG CLEARS WHEN YOU HIT REDIRECT TO ACTION
                    HttpContext.Session.SetString("Email", newUser.Email);
                    return RedirectToAction("Success");
                }
                else
                {
                    return View("Login");
                }
            }

            [Route("Success")]
            [HttpGet]
            public IActionResult Success()
            {

                // VIEWBACK HAS TO GO SOMEWHERE WHERE THERE IS A VIEW NOT A REDIRECT  
                ViewBag.Email = HttpContext.Session.GetString("Email");

                return View("Success");
            }

            [Route("Clear")]
            [HttpGet]

            public IActionResult Clear()
            {
                HttpContext.Session.Clear();
                return View("Index");
            }
        }
    }
}
