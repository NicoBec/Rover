using Rover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rover.Controllers
{
    public class UnitMontlyController : Controller
    {
        private RoverEntities db = new RoverEntities();

        // GET: UnitMontly
        public ActionResult Index()
        {
            var months = db.BillMonths;
            return View();
        }

        // GET: UnitMontly/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UnitMontly/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitMontly/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UnitMontly/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UnitMontly/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UnitMontly/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UnitMontly/Delete/5
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
