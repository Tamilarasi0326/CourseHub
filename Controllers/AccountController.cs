using CourseHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CourseHub.Controllers
{
    public class AccountController : Controller
    {
        public static bool isLoggedIn = false;
        public static bool isAdmin = false;
        public static bool isUser = false; 
        CmsDBEntities db = new CmsDBEntities();
        // GET: Account
        public ActionResult Index()
        {
            if (isLoggedIn == false || isAdmin == false)
                return HttpNotFound();
            return View();
            
        }

        [HttpGet]

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(UserInfo ur)
        {
            if (ModelState.IsValid)
            {
                if (db.UserInfo.Any(x => x.Email == ur.Email))
                {
                    ViewBag.Message = "Email already registered";
                }
                else
                {
                    db.UserInfo.Add(ur);
                    db.SaveChanges();
                    Response.Write("<script>alert('Registration Successful')</script>");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(MyLogin l)
        {
            var query = db.UserInfo.SingleOrDefault(m => m.Email == l.Email && m.Password == l.Password);
            var admin = db.AdminRegister.SingleOrDefault(m => m.Email == l.Email && m.Password == l.Password);
            if (query != null)
            {
                Session["IsLoggedIn"] = true;
                Session["IsUser"] = true;
                Session["Email"] = query.Email;

                return RedirectToAction("CourseView", "Course");
            }
            else if (admin != null)
            {
                Session["IsLoggedIn"] = true;
                Session["IsAdmin"] = true;
                Session["Email"] = admin.Email;

                return RedirectToAction("Index", "Course");

            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Credentials";
                return View();

            }
            /*if (query != null)
            {
                Response.Write("<script>alert('Login Successful')</script>");
                return RedirectToAction("Index","Home");
            }
            else
            {
                Response.Write("<script>alert('Invalid Credentials')</script>");
            }
            return View();*/
        }

    }

}
   

