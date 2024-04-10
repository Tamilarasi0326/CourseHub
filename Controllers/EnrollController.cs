using CourseHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseHub.Controllers
{
    public class EnrollController : Controller
    {
        private CmsDBEntities dB = new CmsDBEntities();
        // GET: Enroll
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EnrollView() 
        {
            return View(dB.OrderDetails.ToList());
        }

        public ActionResult Delete(int id)
        {
            using (CmsDBEntities db = new CmsDBEntities())
            {
                db.OrderDetails.Remove(db.OrderDetails.Find(id));
                db.SaveChanges();
                return RedirectToAction("EnrollView");
            }
        }
    }
}