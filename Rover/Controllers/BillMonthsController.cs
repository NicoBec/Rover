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
                using (var dbContextScope = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.BillMonths.Add(billMonth);
                        db.SaveChanges();
                        var units = db.Units;

                        foreach (var item in units)
                        {
                            var newBill = new Bill() { BillMonthID = billMonth.BillMonthID, UnitID = item.UnitID, BillTotal = 0 };
                            db.Bills.Add(newBill);
                            db.SaveChanges();

                            var billItems = db.BillableItems.Where(x => x.UnitID == item.UnitID);
                            foreach (var billItem in billItems)
                            {
                                db.BillItems.Add(new BillItem() { BillableItemID = billItem.BillableItemID, BillItemAmmount = billItem.Amount, BillId = newBill.BillID });
                                newBill.BillTotal += billItem.Amount;
                            }
                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextScope.Rollback();
                        throw;
                    }
                    dbContextScope.Commit();


                }
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
            using (var dbContextScope = db.Database.BeginTransaction())
            {
                try
                {
                    BillMonth billMonth = db.BillMonths.Find(id);
                    var bills = db.Bills.Where(x => x.BillMonthID == billMonth.BillMonthID);

                    foreach (var item in bills)
                    {
                        db.BillItems.RemoveRange(item.BillItems);
                    }
                    db.SaveChanges();

                    db.Bills.RemoveRange(bills);
                    db.SaveChanges();

                    db.BillMonths.Remove(billMonth);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    dbContextScope.Rollback();
                    throw;
                }
                dbContextScope.Commit();
            }
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
