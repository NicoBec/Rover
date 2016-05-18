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
    public class BillMonthsController : Controller
    {
        private RoverEntities db = new RoverEntities();

        // GET: BillMonths
        public ActionResult Index()
        {
            return View(db.BillMonths.ToList());
        }

        // GET: BillMonths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillMonth billMonth = db.BillMonths.Find(id);
            if (billMonth == null)
            {
                return HttpNotFound();
            }
            return View(billMonth);
        }

        // GET: BillMonths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillMonths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillMonthID,BillMonthDate,BillMonthDesc,IsActiveBillMonth")] BillMonth billMonth)
        {
            if (ModelState.IsValid)
            {
                db.BillMonths.Add(billMonth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billMonth);
        }

        // GET: BillMonths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillMonth billMonth = db.BillMonths.Find(id);
            if (billMonth == null)
            {
                return HttpNotFound();
            }
            return View(billMonth);
        }

        // POST: BillMonths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillMonthID,BillMonthDate,BillMonthDesc,IsActiveBillMonth")] BillMonth billMonth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billMonth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billMonth);
        }

        // GET: BillMonths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillMonth billMonth = db.BillMonths.Find(id);
            if (billMonth == null)
            {
                return HttpNotFound();
            }
            return View(billMonth);
        }

        // POST: BillMonths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillMonth billMonth = db.BillMonths.Find(id);
            db.BillMonths.Remove(billMonth);
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
