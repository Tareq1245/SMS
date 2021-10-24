
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp2020.Models;
using WebApp2020.ViewModels;
using WebApp2020.Public;


namespace WebApp2020.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register
        public ActionResult Register()
        {
            ViewBag.Title = "Register Page";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "UserID,UserName,password,Email")]AdminUser admin)
        {
            if (ModelState.IsValid)
            {
                using (StudentDB2Entities db = new StudentDB2Entities())
                {
                    db.AdminUsers.Add(admin);
                    await db.SaveChangesAsync();
                    // if Register succeeds, go to Login
                    return RedirectToAction("Login");
                }
            }
            //if Register fails, go to the current 〔Register〕 page
            return View(admin);
        }

       
        //GET : Register2
        public ActionResult Register2()
        {
            ViewBag.Title = "Register Here";
            return View();
        }
        //POST: Register2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register2([Bind(Include = "UserID,UserName,password,ConfirmPassword,Email")]AdminUserRegisterEntity registerAdmin)
        {
            // if has passed the model validation
            if (ModelState.IsValid)
            {
                using (StudentDB2Entities db = new StudentDB2Entities())
                {
                    //Create an AdminUser object "admin" based on 
                    // ViewModels.AdminUserRegisterEntity object 
                    //         -- "registerAdmin"
                    AdminUser admin = new AdminUser
                    {
                        UserID = registerAdmin.UserID,
                        UserName = registerAdmin.UserName,
                        password = registerAdmin.password,
                        Email = registerAdmin.Email
                    };
                    db.AdminUsers.Add(admin);
                    await db.SaveChangesAsync();
                    // if Register succeeds, go to Login
                    return RedirectToAction("Login");
                }
            }
            //if not passed model validation, 
            //  return to the current 〔Register〕 page
            //     with validation error infomation displayed!
            return View(registerAdmin);
        }

        //GET: Login
        public ActionResult Login()
        {
            //if have logined successfully, go to Page "Success"
            //                        else, return to Page "Login"
            if (Session["UserID"] != null)
                return RedirectToAction("Success");
            else
                return View();
        }

      

        //POST Register/Login
        //Method B: use remote validation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserID,password")]AdminUserLoginEntity loginAdmin)
        {
            // if has passed the model validation
            if (ModelState.IsValid)
            {
                //login-user info saved in Session objects:

                string username;
                using (StudentDB2Entities db = new StudentDB2Entities())
                {
                    AdminUser admin = db.AdminUsers.Single(a => a.UserID == loginAdmin.UserID);
                    username = admin.UserName;
                }

                //Set Session Objects for the current Login User.
                Session["UserID"] = loginAdmin.UserID;
                Session["UserName"] = username;
                // Login Successfully: go to page "Success"
                return RedirectToAction("Success");
            }

            return View(loginAdmin);
        }

       

        //GET: Success (ONLY for users successfully logined
        [MyAuthorize]  //ALT + Enter KEY, show the prompt for 【using namespace】
        public ActionResult Success()
        {
            return View();
        }



        //GET: Logout
        public ActionResult Logout()
        {
            //Session["UserID"] = null;
            //Session["UserName"] = null;
            //OR:
            Session.Clear();
            return View();
        }

        //The following two methods is used for remote validation
        //     in page 【Register】 :
        public JsonResult CheckUserID4Register(string UserID)
        {
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                //to judge if the input of UserID exists
                //  in the λ-expression, a ∈ db.AdminUser
                bool newUserID = db.AdminUsers
                  .SingleOrDefault(a => a.UserID == UserID) == null;
                return Json(newUserID, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckEmail4Register(string Email)
        {
            //to judge if the input of UserID exists
            //  in the λ-expression, a ∈ db.AdminUser
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool newEmail = db.AdminUsers
                  .SingleOrDefault(a => a.Email == Email) == null;
                return Json(newEmail, JsonRequestBehavior.AllowGet);
            }
        }

        //The following two methods used for remote validation
        //     in page 【Login】:
        public JsonResult CheckUserID(string UserID)
        {
            //Connect to StudentDB2 database
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool existUserID = db.AdminUsers
                  .SingleOrDefault(a => a.UserID == UserID) != null;
                return Json(existUserID, JsonRequestBehavior.AllowGet);
            }
        }
        //CheckPassword is used for remote validation for password
        //     in page 【Login】:
        public JsonResult CheckPassword(string password, string UserID)
        {
            //Entity Framework : ORM (Object-Relation Mapping)
            // OO, Entity Class & its object
            // Relation--RDBMS SQL Server/MySQL, etc.
            //  We can use Lambda Expression
            //         with the LINQ link method 
            using (StudentDB2Entities db = new StudentDB2Entities())
            {
                bool matchPassword = db.AdminUsers.
                  SingleOrDefault(a => a.UserID == UserID && a.password == password) != null;
                return Json(matchPassword, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

