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
    public class BillItemTypesController : Controller
    {
        private RoverEntities db = new RoverEntities();

        // GET: BillItemTypes
        public ActionResult Index()
        {
            return View(db.BillItemTypes.ToList());
        }

        // GET: BillItemTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItemType billItemType = db.BillItemTypes.Find(id);
            if (billItemType == null)
            {
                return HttpNotFound();
            }
            return View(billItemType);
        }

        // GET: BillItemTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillItemTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillItemID,BillItemDesc")] BillItemType billItemType)
        {
            if (ModelState.IsValid)
            {
                db.BillItemTypes.Add(billItemType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billItemType);
        }

        // GET: BillItemTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItemType billItemType = db.BillItemTypes.Find(id);
            if (billItemType == null)
            {
                return HttpNotFound();
            }
            return View(billItemType);
        }

        // POST: BillItemTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillItemID,BillItemDesc")] BillItemType billItemType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billItemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billItemType);
        }

        // GET: BillItemTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillItemType billItemType = db.BillItemTypes.Find(id);
            if (billItemType == null)
            {
                return HttpNotFound();
            }
            return View(billItemType);
        }

        // POST: BillItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillItemType billItemType = db.BillItemTypes.Find(id);
            db.BillItemTypes.Remove(billItemType);
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
