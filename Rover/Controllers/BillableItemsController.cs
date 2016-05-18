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
    public class BillableItemsController : Controller
    {
        private RoverEntities db = new RoverEntities();

        // GET: BillableItems
        public ActionResult Index()
        {
            var billableItems = db.BillableItems.Include(b => b.BillItemType).Include(b => b.Unit);
            return View(billableItems.ToList());
        }

        // GET: BillableItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillableItem billableItem = db.BillableItems.Find(id);
            if (billableItem == null)
            {
                return HttpNotFound();
            }
            return View(billableItem);
        }

        // GET: BillableItems/Create
        public ActionResult Create()
        {
            ViewBag.BillableItemType = new SelectList(db.BillItemTypes, "BillItemID", "BillItemDesc");
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName");
            return View();
        }

        // POST: BillableItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillableItemID,BillableItemType,UnitID,Amount,StartDate,EndDate,ActiveBillableItem")] BillableItem billableItem)
        {
            if (ModelState.IsValid)
            {
                db.BillableItems.Add(billableItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillableItemType = new SelectList(db.BillItemTypes, "BillItemID", "BillItemDesc", billableItem.BillableItemType);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", billableItem.UnitID);
            return View(billableItem);
        }

        // GET: BillableItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillableItem billableItem = db.BillableItems.Find(id);
            if (billableItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillableItemType = new SelectList(db.BillItemTypes, "BillItemID", "BillItemDesc", billableItem.BillableItemType);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", billableItem.UnitID);
            return View(billableItem);
        }

        // POST: BillableItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillableItemID,BillableItemType,UnitID,Amount,StartDate,EndDate,ActiveBillableItem")] BillableItem billableItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billableItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillableItemType = new SelectList(db.BillItemTypes, "BillItemID", "BillItemDesc", billableItem.BillableItemType);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", billableItem.UnitID);
            return View(billableItem);
        }

        // GET: BillableItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillableItem billableItem = db.BillableItems.Find(id);
            if (billableItem == null)
            {
                return HttpNotFound();
            }
            return View(billableItem);
        }

        // POST: BillableItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillableItem billableItem = db.BillableItems.Find(id);
            db.BillableItems.Remove(billableItem);
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
