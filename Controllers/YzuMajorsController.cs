using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp2020.Models;

namespace WebApp2020.Controllers
{
    public class YzuMajorsController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        // GET: YzuMajors
        public ActionResult Index()
        {
            var yzuMajors = db.YzuMajors.Include(y => y.YzuSchool);
            return View(yzuMajors.ToList());
        }

        // GET: YzuMajors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YzuMajor yzuMajor = db.YzuMajors.Find(id);
            if (yzuMajor == null)
            {
                return HttpNotFound();
            }
            return View(yzuMajor);
        }

        // GET: YzuMajors/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(db.YzuSchools, "SchoolID", "SchoolName");
            return View();
        }

        // POST: YzuMajors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajorID,MajorName,SchoolID")] YzuMajor yzuMajor)
        {
            if (ModelState.IsValid)
            {
                db.YzuMajors.Add(yzuMajor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolID = new SelectList(db.YzuSchools, "SchoolID", "SchoolName", yzuMajor.SchoolID);
            return View(yzuMajor);
        }

        // GET: YzuMajors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YzuMajor yzuMajor = db.YzuMajors.Find(id);
            if (yzuMajor == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolID = new SelectList(db.YzuSchools, "SchoolID", "SchoolName", yzuMajor.SchoolID);
            return View(yzuMajor);
        }

        // POST: YzuMajors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorID,MajorName,SchoolID")] YzuMajor yzuMajor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yzuMajor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolID = new SelectList(db.YzuSchools, "SchoolID", "SchoolName", yzuMajor.SchoolID);
            return View(yzuMajor);
        }

        // GET: YzuMajors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YzuMajor yzuMajor = db.YzuMajors.Find(id);
            if (yzuMajor == null)
            {
                return HttpNotFound();
            }
            return View(yzuMajor);
        }

        // POST: YzuMajors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YzuMajor yzuMajor = db.YzuMajors.Find(id);
            db.YzuMajors.Remove(yzuMajor);
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
