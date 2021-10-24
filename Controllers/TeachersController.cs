using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp2020.Models;
using WebApp2020.Public;
using System.IO;

namespace WebApp2020.Controllers
{
    [MyAuthorize]
    public class TeachersController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.YzuMajor);
            return View(teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName");
            ViewBag.Title = new SelectList(db.Titles, "Title1","Title1");
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1","Sex1");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherID,TeacharName,Password,Sex,MajorID,Title,Photo,Email,REMARK")] Teacher teacher, HttpPostedFileBase Photo)
        {  //                   Bind an additional field "Photo"    ▲
            if (ModelState.IsValid)
            {
                var photoFileName = "N/A";
                if (Photo == null)
                {
                    Response.Write("<script>alert('Sorry the Photo Location is Not Available');</script>");

                }
                else
                {
                    //We must ensure the "Upload" directory exists in the root directory of the website!
                    //   OR else, exception will occur and the create will fail!
                    photoFileName = Path.Combine(Request.MapPath("~/Images"), Path.GetFileName(Photo.FileName));

                }
                try
                {
                    Photo.SaveAs(photoFileName); //the file for "Photo" is saved in ~/Upload!
                    teacher.Photo = Path.GetFileName(Photo.FileName); // student1.jpg
                                                                      //student.Photo = photoFileName;

                }
                catch
                {
                    return Content("Abnormal Picture Uploaded!", "text/plain");
                }
                //IF exception occurred, Check if the directory "~/Upload" exists!!

                //NOT CHANGED
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName", teacher.MajorID);
            ViewBag.Title = new SelectList(db.Titles, "Title1","Title1", teacher.Title);
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1","Sex1", teacher.Sex);
            return View(teacher);
        }







        // GET: Teachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName", teacher.MajorID);
            ViewBag.Title = new SelectList(db.Titles, "Title1","Title1", teacher.Title);
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1","Sex1", teacher.Sex);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherID,TeacharName,Password,Sex,MajorID,Title,Photo,Email,REMARK")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName", teacher.MajorID);
            ViewBag.Title = new SelectList(db.Titles, "Title1", "Title1", teacher.Title);
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1","Sex1", teacher.Sex);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //if (disposing)
        //{
        //  db.Dispose();
        //}
        //  base.Dispose(disposing);
        //}


        public JsonResult CheckUserID4Register(string TeacherID)
        {
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                //to judge if the input of UserID exists
                //  in the λ-expression, a ∈ db.AdminUser
                bool newTeacherId = db.Teachers
                  .SingleOrDefault(a => a.TeacherID == TeacherID) == null;
                return Json(newTeacherId, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckEmail4Register(string Email)
        {
            //to judge if the input of UserID exists
            //  in the λ-expression, a ∈ db.AdminUser
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool newEmail = db.Teachers
                  .SingleOrDefault(a => a.Email == Email) == null;
                return Json(newEmail, JsonRequestBehavior.AllowGet);
            }
        }

        //The following two methods used for remote validation
        //     in page 【Login】:
        public JsonResult CheckUserID(string TeacherID)
        {
            //Connect to StudentDB2 database
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool existTeacherID = db.Teachers
                  .SingleOrDefault(a => a.TeacherID == TeacherID) != null;
                return Json(existTeacherID, JsonRequestBehavior.AllowGet);
            }
        }
        //CheckPassword is used for remote validation for password
        //     in page 【Login】:
        public JsonResult CheckPassword(string password, string TeacherID)
        {
            //Entity Framework : ORM (Object-Relation Mapping)
            // OO, Entity Class & its object
            // Relation--RDBMS SQL Server/MySQL, etc.
            //  We can use Lambda Expression
            //         with the LINQ link method 
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool matchPassword = db.Teachers.
                  SingleOrDefault(a => a.TeacherID == TeacherID && a.Password == password) != null;
                return Json(matchPassword, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
