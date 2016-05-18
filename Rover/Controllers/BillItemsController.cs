using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rover.Models;

namespace Rover.Controllers
{
    public class BillItemsController : Controller
    {
        private RoverEntities db = new RoverEntities();

        // GET: BillItems
        public ActionResult Index()
        {
            var billItems = db.BillItems.Include(b => b.BillableItem).Include(b => b.Bill);
            return View(billItems.ToList());
        }

        // GET: BillItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return HttpNotFound();
            }
            return View(billItem);
        }

        // GET: BillItems/Create
        public ActionResult Create()
        {
            ViewBag.BillableItemID = new SelectList(db.BillableItems, "BillableItemID", "BillableItemID");
            ViewBag.BillId = new SelectList(db.Bills, "BillID", "BillID");
            return View();
        }

        // POST: BillItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillItemID,BillableItemID,BillItemAmmount,BillId")] BillItem billItem)
        {
            if (ModelState.IsValid)
            {
                db.BillItems.Add(billItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillableItemID = new SelectList(db.BillableItems, "BillableItemID", "BillableItemID", billItem.BillableItemID);
            ViewBag.BillId = new SelectList(db.Bills, "BillID", "BillID", billItem.BillId);
            return View(billItem);
        }

        // GET: BillItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillableItemID = new SelectList(db.BillableItems, "BillableItemID", "BillableItemID", billItem.BillableItemID);
            ViewBag.BillId = new SelectList(db.Bills, "BillID", "BillID", billItem.BillId);
            return View(billItem);
        }

        // POST: BillItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillItemID,BillableItemID,BillItemAmmount,BillId")] BillItem billItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillableItemID = new SelectList(db.BillableItems, "BillableItemID", "BillableItemID", billItem.BillableItemID);
            ViewBag.BillId = new SelectList(db.Bills, "BillID", "BillID", billItem.BillId);
            return View(billItem);
        }

        // GET: BillItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return HttpNotFound();
            }
            return View(billItem);
        }

        // POST: BillItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillItem billItem = db.BillItems.Find(id);
            db.BillItems.Remove(billItem);
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
