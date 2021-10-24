using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp2020.Models;
using WebApp2020.Public;

namespace WebApp2020Spring.Controllers
{
    [MyAuthorize]
    public class StudentsController : Controller
    {
        private StudentDB2Entities db = new StudentDB2Entities();

        private List<Student> GetStudents(int pageIndex, int pageSize)
        {
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageSize"] = pageSize;
            //var student = db.Student; //.Include(s => s.YzuMajor);

            ViewData["totalCount"] = db.Students.Count();
            var students = db.Students
              .OrderBy(s => s.StudentID)
              .Skip(pageSize * (pageIndex - 1))
              .Take(pageSize).ToList();
            return students;
        }
        // GET: Index
        //     Action method's parameters with default values
        public ActionResult Index(int pageIndex = 1, int pageSize = 2)
        {
            //ViewBag.IsAjax = false;
            if (Request.IsAjaxRequest())
            {
                //ViewBag.IsAjax = true;
                return PartialView(GetStudents(pageIndex, pageSize));// return PartialView(..)
            }
            else
                return View(GetStudents(pageIndex, pageSize));
        }
        // GET : AjaxIndex
        public ActionResult AjaxIndex(int pageIndex, int pageSize)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(GetStudents(pageIndex, pageSize));
            }
            else
                return View(GetStudents(pageIndex, pageSize));
        }


        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName");
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1", "Sex1");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include =
      "StudentID,StudentName,Sex,DateOfBirth,MajorID,Email,Pwd,Photo")] Student student,
          HttpPostedFileBase Photo)
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
                    photoFileName = Path.Combine(Request.MapPath("~/Upload"), Path.GetFileName(Photo.FileName));
                    
                }
                try
                {
                    Photo.SaveAs(photoFileName); //the file for "Photo" is saved in ~/Upload!
                    student.Photo = Path.GetFileName(Photo.FileName); // student1.jpg
                    //student.Photo = photoFileName;
                                                                      
                }
                catch
                {
                    return Content("Abnormal Picture Uploaded!", "text/plain");
                }
                //IF exception occurred, Check if the directory "~/Upload" exists!!

                //NOT CHANGED
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName", student.MajorID);
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1", "Sex1",student.Sex);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName", student.MajorID);
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1", "Sex1", student.Sex);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
      "StudentID,StudentName,Sex,DateOfBirth,MajorID,Email,Pwd")]Student student)
        {  //                   Bind an additional field "Photo"    ▲
            if (ModelState.IsValid)
            {
                //Function to deal with change the photo ....

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.YzuMajors, "MajorID", "MajorName", student.MajorID);
            ViewBag.Sex = new SelectList(db.Sexes, "Sex1", "Sex1", student.Sex);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

        public JsonResult CheckUserID4Register(string StudentID)
        {
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                //to judge if the input of UserID exists
                //  in the λ-expression, a ∈ db.AdminUser
                bool newStudentId = db.Students
                  .SingleOrDefault(a => a.StudentID == StudentID) == null;
                return Json(newStudentId, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckEmail4Register(string Email)
        {
            //to judge if the input of UserID exists
            //  in the λ-expression, a ∈ db.AdminUser
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool newEmail = db.Students
                  .SingleOrDefault(a => a.Email == Email) == null;
                return Json(newEmail, JsonRequestBehavior.AllowGet);
            }
        }

        //The following two methods used for remote validation
        //     in page 【Login】:
        public JsonResult CheckUserID(string StudentID)
        {
            //Connect to StudentDB2 database
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool existStudentID = db.Students
                  .SingleOrDefault(a => a.StudentID == StudentID) != null;
                return Json(existStudentID, JsonRequestBehavior.AllowGet);
            }
        }
        //CheckPassword is used for remote validation for password
        //     in page 【Login】:
        public JsonResult CheckPassword(string password, string StudentID)
        {
            //Entity Framework : ORM (Object-Relation Mapping)
            // OO, Entity Class & its object
            // Relation--RDBMS SQL Server/MySQL, etc.
            //  We can use Lambda Expression
            //         with the LINQ link method 
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool matchPassword = db.Students.
                  SingleOrDefault(a => a.StudentID == StudentID && a.Pwd == password) != null;
                return Json(matchPassword, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
