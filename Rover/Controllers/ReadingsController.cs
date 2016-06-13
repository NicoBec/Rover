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
    public class ReadingsController : Controller
    {
        private RoverEntities db = new RoverEntities();

        // GET: Readings
        public ActionResult Index()
        {
            var readings = db.Readings.Include(r => r.BillMonth).Include(r => r.Unit);
            return View(readings.ToList());
        }

        // GET: Readings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reading reading = db.Readings.Find(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }

        // GET: Readings/Create
        public ActionResult Create(int nextUnit = 0)
        {
            var activeBillMonth = db.BillMonths.Where(x => x.IsActiveBillMonth == true).Max(x => x.BillMonthID);
            ViewBag.BillMonthID = new SelectList(db.BillMonths, "BillMonthID", "BillMonthDesc", activeBillMonth);
            if (nextUnit == 0)
            {
                ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName");
            }
            else
            {
                var t = db.Units.SingleOrDefault(x => x.UnitID == nextUnit).UnitID;
                ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", t);
            }

            return View();
        }

        // POST: Readings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReadingID,ReadingDate,ReadingValue,UnitID,ReadingVariance,BillMonthID")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                var checkExist = db.Readings.SingleOrDefault(x => x.UnitID == reading.UnitID && x.BillMonthID == reading.BillMonthID);
                if (checkExist == null)
                {
                    db.Readings.Add(reading);
                    db.SaveChanges();

                    var activeBillMonth = db.BillMonths.Where(x => x.IsActiveBillMonth == true).Max(x => x.BillMonthID);
                    var doneReadings = db.Readings.Where(x => x.BillMonthID == activeBillMonth).Select(x => x.UnitID).ToList();

                    var unitsnotDone = db.Units.Where(x => !doneReadings.Contains(x.UnitID)).ToList();
                    var unitToDo = 0;
                    if (unitsnotDone.Count > 0)
                    {
                        unitToDo = unitsnotDone.Select(x => x.UnitID).Min(x => x);
                    }

                    if (unitToDo > 0)
                    {
                        return RedirectToAction("Create", new { nextUnit = unitToDo });
                    }
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Duplicate = "The Units reading has been captured for this month, please check!";
                    ViewBag.DuplicateId = checkExist.ReadingID;
                }
            }

            ViewBag.BillMonthID = new SelectList(db.BillMonths, "BillMonthID", "BillMonthDesc", reading.BillMonthID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", reading.UnitID);

            return View(reading);
        }

        // GET: Readings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reading reading = db.Readings.Find(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillMonthID = new SelectList(db.BillMonths, "BillMonthID", "BillMonthDesc", reading.BillMonthID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", reading.UnitID);
            return View(reading);
        }

        // POST: Readings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReadingID,ReadingDate,ReadingValue,UnitID,ReadingVariance,BillMonthID")] Reading reading)
        {
            if (ModelState.IsValid)
            {
                var checkExist = db.Readings.SingleOrDefault(x => x.UnitID == reading.UnitID && x.BillMonthID == reading.BillMonthID);
                db.Entry(checkExist).State = EntityState.Detached;
                if (checkExist == null || checkExist.ReadingID == reading.ReadingID)
                {
                    db.Entry(reading).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Duplicate = "The Units reading has been captured for this month, please check!";
                    ViewBag.DuplicateId = checkExist.ReadingID;
                }

            }
            ViewBag.BillMonthID = new SelectList(db.BillMonths, "BillMonthID", "BillMonthDesc", reading.BillMonthID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "UnitName", reading.UnitID);
            return View(reading);
        }

        // GET: Readings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reading reading = db.Readings.Find(id);
            if (reading == null)
            {
                return HttpNotFound();
            }
            return View(reading);
        }

        // POST: Readings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reading reading = db.Readings.Find(id);
            db.Readings.Remove(reading);
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
