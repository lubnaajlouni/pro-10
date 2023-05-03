using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lubnamaster.Models;
using Microsoft.AspNet.Identity;

namespace lubnamaster.Controllers
{
    public class OrdersController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: Orders
        public ActionResult Index()
        {
            ViewBag.x = "Orders";
            var orders = db.Orders.Include(o => o.AspNetUser);


            //string userId = User.Identity.GetUserId();
            //var itemofferd = db.OrderDetails.Where(c => c.ItemId == c.Itemeoffered.ColorId && c.OrderId == c.Order.OrderId).Sum(o => o.Quantity * o.Itemeoffered.Item.ItemPrice);
        

            //// Store the calculated total in ViewBag
            //ViewBag.Total = itemofferd.ToString();
            
            return View(orders.ToList());
        }



        [HttpPost]
        public ActionResult Index(string search5)
        {

            if (search5 != null)
            {
                var abumahmood = db.Orders.Where(x => x.FirstName.Contains(search5)).ToList();
                return View(abumahmood);
            }
            else
            {
                var abumahmood = db.Orders.ToList();

            }

            var orders = db.Orders.Include(o => o.AspNetUser);
            return View(orders.ToList());

        }



        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            
            List<OrderDetail> orderDetails = db.OrderDetails.Where(od => od.OrderId == id).ToList();

            if (orderDetails.Count == 0)
            {
                return HttpNotFound();
            }

           

            return View(orderDetails);
        }





        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,FirstName,LastName,City,Adress,UserId,Email,PhoneNumber,PaymentMethod,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,FirstName,LastName,City,Adress,UserId,Email,PhoneNumber,PaymentMethod,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
