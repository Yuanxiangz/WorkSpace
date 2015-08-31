using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETTest.Models;

namespace ASPNETTest.Controllers
{
    public class table3Controller : Controller
    {
        private testContext db = new testContext();

        //
        // GET: /table3/

        public ActionResult Index()
        {
            return View(db.table3.ToList());
        }

        //
        // GET: /table3/Details/5

        public ActionResult Details(string id = null)
        {
            table3 table3 = db.table3.Find(id);
            if (table3 == null)
            {
                return HttpNotFound();
            }
            return View(table3);
        }

        //
        // GET: /table3/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /table3/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(table3 table3)
        {
            if (ModelState.IsValid)
            {
                db.table3.Add(table3);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table3);
        }

        //
        // GET: /table3/Edit/5

        public ActionResult Edit(string id = null)
        {
            table3 table3 = db.table3.Find(id);
            if (table3 == null)
            {
                return HttpNotFound();
            }
            return View(table3);
        }

        //
        // POST: /table3/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(table3 table3)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table3).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table3);
        }

        //
        // GET: /table3/Delete/5

        public ActionResult Delete(string id = null)
        {
            table3 table3 = db.table3.Find(id);
            if (table3 == null)
            {
                return HttpNotFound();
            }
            return View(table3);
        }

        //
        // POST: /table3/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            table3 table3 = db.table3.Find(id);
            db.table3.Remove(table3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}