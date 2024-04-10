using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using CourseHub.Models;
using Microsoft.Ajax.Utilities;

namespace CourseHub.Controllers
{
    public class CartController : Controller
    {
        CmsDBEntities dB = new CmsDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToCart(int CourseId) 
        {
            if (Session["cart"] == null)
            {
                List<CourseItem> cart = new List<CourseItem>();
                //CourseItem item = new CourseItem();
                //item.course = dB.Course.Find(CourseId);
                //cart.Add(item);
                cart.Add(new CourseItem() { course = dB.Course.Find(CourseId) });
                Session["cart"] = cart;
            }
            else 
            {
                List<CourseItem> cart = (List<CourseItem>) Session["cart"];
                int index = IsInCart(CourseId);
                if (index != -1)
                {

                }
                else 
                {
                    cart.Add(new CourseItem() { course = dB.Course.Find(CourseId) });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int CourseId) 
        {
            List<CourseItem> cart = (List<CourseItem>)Session["cart"];
            int index = IsInCart(CourseId);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        public int IsInCart(int CourseId) 
        {
            List<CourseItem> cart = (List<CourseItem>)Session["cart"];
            for (int i = 0; i < cart.Count; i++) 
            {
                if (cart[i].course.CourseId == CourseId) 
                {
                    return i;
                }
            }
            return -1;
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            int OrderId = 0;
            List<CourseItem> cart = (List<CourseItem>)Session["cart"];
            Orders orderObj = new Orders()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddmmyyyyHHmmsss}", DateTime.Now)
            };

            dB.Orders.Add(orderObj);
            dB.SaveChanges();

            OrderId = orderObj.OrderId;

            foreach (var item in (List<CourseItem>)Session["cart"] )
            {
                OrderDetails objOrderDetail = new OrderDetails();
                objOrderDetail.orderid = OrderId;
                objOrderDetail.courseid = item.course.CourseId;
                objOrderDetail.coursename = item.course.CourseName;
                dB.OrderDetails.Add(objOrderDetail);
                dB.SaveChanges();
            }

            Session["cart"] = null;
            return RedirectToAction("PaidView");
        }

        public ActionResult PaidView()
        {
            return View();
        }

        /*public ActionResult Checkout(int id) 
        {
            List<CourseItem> cart = (List<CourseItem>)Session["cart"];
            
            if (cart == null || cart.Count == 0) 
            {
                return RedirectToAction("EmptyCart");
            }

            var course = dB.Course.Where(d => d.CourseId == id).Select(c => c.CourseId).FirstOrDefault();
            var coursename = (dB.Course.Where(d => d.CourseId == id).Select(c => c.CourseName)).FirstOrDefault();
            var courseprice = (dB.Course.Where(d => d.CourseId == id).Select(c => c.CoursePrice)).FirstOrDefault();

            var pay = new Pay
            {
                CourseId = course,
                CourseName = coursename,
                CoursePrice = courseprice,
                EnrollDate = DateTime.Now,

            };

            dB.Pay.Add(pay);
            dB.SaveChanges();

            Session["cart"] = null; 
            
            return RedirectToAction("PaidView", new { EnrollId = pay.EnrollId });   
               
        }*/

      
        
    }
}