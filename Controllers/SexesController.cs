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
    public class SexesController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        // GET: Sexes
        public ActionResult Index()
        {
            return View(db.Sexes.ToList());
        }

        // GET: Sexes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sex sex = db.Sexes.Find(id);
            if (sex == null)
            {
                return HttpNotFound();
            }
            return View(sex);
        }

        // GET: Sexes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sexes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sex1")] Sex sex)
        {
            if (ModelState.IsValid)
            {
                db.Sexes.Add(sex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sex);
        }

        // GET: Sexes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sex sex = db.Sexes.Find(id);
            if (sex == null)
            {
                return HttpNotFound();
            }
            return View(sex);
        }

        // POST: Sexes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sex1")] Sex sex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sex);
        }

        // GET: Sexes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sex sex = db.Sexes.Find(id);
            if (sex == null)
            {
                return HttpNotFound();
            }
            return View(sex);
        }

        // POST: Sexes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sex sex = db.Sexes.Find(id);
            db.Sexes.Remove(sex);
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
