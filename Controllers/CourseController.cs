using CourseHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Net;

namespace CourseHub.Controllers
{
    public class CourseController : Controller
    {
        CmsDBEntities db = new CmsDBEntities();
        // GET: Course
        public ActionResult Index()
        {
            using (CmsDBEntities db = new CmsDBEntities())
            {
                List<Course> CourseList = (from data in db.Course select data).ToList();
                return View(CourseList);
            }
        }

        // GET: Course/Details/5

        public ActionResult CourseView()
        {
            using (CmsDBEntities db = new CmsDBEntities())
            {
                List<Course> courses = (from data in db.Course select data).ToList();
                return View(courses);
            }
        }


        public ActionResult Details(int? id)
        {
            /*return View();*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
               return HttpNotFound();
            }
            return View(course);
            
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View(new Course()); //passing a object of Course class
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(Course course, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add insert logic here
                string extension = Path.GetExtension(postedFile.FileName);
                if (extension.Equals(".jpg") || extension.Equals(".png"))
                {
                    string filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                    string savepath = Server.MapPath("~/Content/Images/");
                    postedFile.SaveAs(savepath + filename);
                    course.CoursePic = filename;
                    using (CmsDBEntities db = new CmsDBEntities())
                    {
                        db.Course.Add(course);
                        db.SaveChanges();
                    }
                }
                else
                {
                    return Content("<h1>You can only upload JPG or PNG Files!!</h1>");
                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            using (CmsDBEntities db = new CmsDBEntities()) 
            {
                //Course course = (from data in db.Course where data.CourseId == id select data).Single();
                Course course = db.Course.Find(id);
                return View(course);
            }
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(Course course, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here
                string filename = "";
                if (postedFile != null)
                {
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/Content/Images/");
                        postedFile.SaveAs(savepath + filename);
                    }
                }
                using (CmsDBEntities db = new CmsDBEntities()) 
                {
                    Course course1 = db.Course.Find(course.CourseId);
                    course1.CourseName = course.CourseName;
                    course1.CoursePrice = course.CoursePrice;
                    course1.Description = course.Description;
                    course1.CourseAuthor = course.CourseAuthor;
                    if (!filename.Equals("")) 
                    {
                        course1.CoursePic = filename;
                    }
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            using (CmsDBEntities db = new CmsDBEntities())
            {
                db.Course.Remove(db.Course.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
