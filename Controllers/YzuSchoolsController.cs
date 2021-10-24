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
    public class YzuSchoolsController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        // GET: YzuSchools
        public ActionResult Index()
        {
            return View(db.YzuSchools.ToList());
        }

        // GET: YzuSchools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YzuSchool yzuSchool = db.YzuSchools.Find(id);
            if (yzuSchool == null)
            {
                return HttpNotFound();
            }
            return View(yzuSchool);
        }

        // GET: YzuSchools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YzuSchools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolID,SchoolName")] YzuSchool yzuSchool)
        {
            if (ModelState.IsValid)
            {
                db.YzuSchools.Add(yzuSchool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yzuSchool);
        }

        // GET: YzuSchools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YzuSchool yzuSchool = db.YzuSchools.Find(id);
            if (yzuSchool == null)
            {
                return HttpNotFound();
            }
            return View(yzuSchool);
        }

        // POST: YzuSchools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolID,SchoolName")] YzuSchool yzuSchool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yzuSchool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yzuSchool);
        }

        // GET: YzuSchools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YzuSchool yzuSchool = db.YzuSchools.Find(id);
            if (yzuSchool == null)
            {
                return HttpNotFound();
            }
            return View(yzuSchool);
        }

        // POST: YzuSchools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YzuSchool yzuSchool = db.YzuSchools.Find(id);
            db.YzuSchools.Remove(yzuSchool);
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
