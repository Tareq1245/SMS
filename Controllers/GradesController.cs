using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp2020.Models;
using WebApp2020.ViewModels;

namespace WebApp2020Spring.Controllers
{
    public class GradesController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        // GET: Grades
        public ActionResult Index()
        {
            var grade = db.Grades.Include(g => g.Course).Include(g => g.Student);
            return View(grade.ToList());
        }

        // GET: Grades/Details/10002/A001
        public ActionResult Details(string id1, string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id1, id2);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            return View();
        }

        // POST: Grades/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,CourseID,Score,DateOfExam")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                if (grade.DateOfExam == null)
                    grade.DateOfExam = DateTime.Now;
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", grade.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", grade.StudentID);
            return View(grade);
        }

        // GET: Grades/Edit/10002/A001
        public ActionResult Edit(string id1, string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id1, id2);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", grade.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", grade.StudentID);
            return View(grade);
        }

        // POST: Grades/Edit/10002/A001
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,CourseID,Score,DateOfExam")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); //Grades/Index
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", grade.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", grade.StudentID);
            return View(grade); //Grades/Edit/../..
        }

        // GET: Grades/Delete/10002/A001
        public ActionResult Delete(string id1, string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id1, id2);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/10002/A001
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2)
        {
            Grade grade = db.Grades.Find(id1, id2);
            db.Grades.Remove(grade);
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
