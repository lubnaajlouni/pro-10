using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lubnamaster.Models;

namespace lubnamaster.Controllers
{
    public class ReviewsController : Controller
    {
        private storemasterEntities db = new storemasterEntities();

        // GET: Reviews
        public ActionResult Index( )
        {
           



            ViewBag.x = "Reviews";
            var reviews = db.Reviews.Include(r => r.AspNetUser).Include(r => r.Item);
            return View(reviews.ToList());
        }


        [HttpPost]
        public ActionResult Index(string filter, string search5)
        {

            if (filter == "1")
            {
                var ss = db.Reviews.Where(x => x.Approve == false).ToList();
                return View(ss);

            }
            else if (filter == "2")
            {
                var ss = db.Reviews.Where(x => x.Approve == true).ToList();
                return View(ss);
            }
            else if (filter == "3")
            {
                var ss = db.Reviews.ToList();
                return View(ss);
            }
            if (search5 != null)
            {
                var abumahmood = db.Reviews.Where(x => x.Name.Contains(search5)).ToList();
                return View(abumahmood);
            }

            var reviews = db.Reviews.Include(r => r.AspNetUser).Include(r => r.Item);
            return View(reviews.ToList());
        }



            [HttpPost]
        
        public ActionResult lubna(int id, bool? approve)
        {
            var review = db.Reviews.Find(id);
            review.Approve = approve;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        


        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewId,UserId,Name,ItemId,Description,Date,Approve")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", review.UserId);
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", review.ItemId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", review.UserId);
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", review.ItemId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewId,UserId,Name,ItemId,Description,Date,Approve")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", review.UserId);
            ViewBag.ItemId = new SelectList(db.Items, "Item_Id", "Item_Name", review.ItemId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
