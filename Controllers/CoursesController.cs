using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//namespace WebApp2020.Controllers
//{
    //public class CoursesController : Controller
    //{
        // GET: Courses
        //public ActionResult Index()
        //{
      //      return View();
    //    }
  //  }
//}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks; //await db..... & async Task<ActionResult>
using System.Web;
using System.Web.Mvc;
using WebApp2020.Models;
using WebApp2020.ViewModels;

namespace WebApp2020Spring.Controllers
{
    public class CoursesController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,CourseName,Period,Credit")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Course/EnterGrades/A001 
        public ActionResult EnterGrades(string id)
        {
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                List<CourseGrade> grades = db.Grades.Where(g => g.CourseID == id).Select(g => new CourseGrade
                {
                    StudentID = g.StudentID,
                    StudentName = g.Student.StudentName,
                    Score = g.Score
                }).ToList();

                return View(grades);
            }
        }
        //POST: Course/EnterGrades/A001
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnterGrades(string id, FormCollection collection)
        {
            int count = int.Parse(collection["recordNum"]);
            Grade grade = null;
            string sid = ""; //for current StudentID
                             //string cid = ""; //for current CourseID
            short score = 0; //for current Score
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                for (int i = 0; i < count; i++)
                {
                    sid = collection[string.Format("sno{0}", i)].ToString();
                    //cid = id; //from the RouteData
                    short.TryParse(collection[string.Format("score{0}", i)].ToString(), out score); //"abc" ==> 0; "99" ==> 99
                    grade = await db.Grades.FindAsync(sid, id);
                    grade.Score = score;
                }
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CourseName,Period,Credit")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
