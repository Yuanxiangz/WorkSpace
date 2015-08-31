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
    public class Test1Controller : Controller
    {
        private testContext db = new testContext();

        //
        // GET: /Test1/

        public ActionResult Index()
        {
            return View(db.table1.ToList());
        }

        //
        // GET: /Test1/Details/5

        public ActionResult Details(string id = null)
        {
            table1 table1 = db.table1.Find(id);
            if (table1 == null)
            {
                return HttpNotFound();
            }
            return View(table1);
        }

        //
        // GET: /Test1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Test1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(table1 table1)
        {
            if (ModelState.IsValid)
            {
                db.table1.Add(table1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table1);
        }

        //
        // GET: /Test1/Edit/5

        public ActionResult Edit(string id = null)
        {
            table1 table1 = db.table1.Find(id);
            if (table1 == null)
            {
                return HttpNotFound();
            }
            return View(table1);
        }

        //
        // POST: /Test1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(table1 table1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table1);
        }

        //
        // GET: /Test1/Delete/5

        public ActionResult Delete(string id = null)
        {
            table1 table1 = db.table1.Find(id);
            if (table1 == null)
            {
                return HttpNotFound();
            }
            return View(table1);
        }

        //
        // POST: /Test1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            table1 table1 = db.table1.Find(id);
            db.table1.Remove(table1);
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