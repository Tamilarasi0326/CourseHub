using CourseHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace CourseHub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult CourseView() 
        {
            using (CmsDBEntities db = new CmsDBEntities())
            {
                List<Course> courses = (from data in db.Course select data).ToList();
                return View(courses);
            }
        }*/

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}