using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabSimulation.Models;

namespace LabSimulation.Controllers
{

    public class UsersController : Controller
    {
        private LabSimulationDBEntities db = new LabSimulationDBEntities();

        public static HttpCookie cookie = new HttpCookie("Login");

        // GET: Users/SignIn
        public ActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SignIn(string EmailUserName, string Password)
        {

            if (db.Users.Where(m => m.Email == EmailUserName | m.UserName == EmailUserName && m.Password == Password).Any())
            {
                var userAccount = db.View_User.Where(m => m.Email == EmailUserName | m.UserName == EmailUserName).ToList().FirstOrDefault();
                cookie["UserID"] = userAccount.ID.ToString();
                cookie["FullName"] = userAccount.FullName;
                cookie["SchoolName"] = userAccount.SchoolName;
                cookie["IsPremium"] = userAccount.IsPremiumAccount.ToString();
                cookie["UserType"] = userAccount.UserType;
                cookie.Expires = DateTime.Now.AddDays(7d);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Invalid Logged"] = "Email Or Password Is Invalid, Logged Failed";
                return View();
            }
        }


        // GET: Users/SignUp
        public ActionResult SignUp()
        {
            ViewBag.UserTypeID = new SelectList(db.UserTypeAccounts, "ID", "Name");
            return View();
        }

        // POST: Users/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "ID,FirstName,LastName,SchoolName,UserName,Email,Password,Image,IsConfirm,IsPremiumAccount,SchoolID,UserTypeID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("SignIn");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeAccounts, "ID", "Name", user.UserTypeID);
            return View(user);
        }

        [HttpPost]
        public JsonResult CheckIsEmailAvailable(string UserEmail)
        {
            var searchData = db.Users.Where(x => x.Email == UserEmail).SingleOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }


        [HttpPost]
        public JsonResult CheckIsUseNameAvailable(string UserName)
        {
            var searchData = db.Users.Where(x => x.UserName == UserName).SingleOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult IsUserNameAvailable(string UserName, int ID = 0)
        {
            if (ID != 0)
            {
                if (db.Users.ToList().Exists(m => m.UserName == UserName && m.ID == ID))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(!db.Users.ToList().Exists(m => m.UserName == UserName), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(!db.Users.ToList().Exists(m => m.UserName == UserName), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsEmailAvailable(string Email, int ID = 0)
        {
            if(ID != 0)
            {
                if(db.Users.ToList().Exists(m => m.Email == Email && m.ID == ID))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(!db.Users.ToList().Exists(m => m.Email == Email), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(!db.Users.ToList().Exists(m => m.Email == Email), JsonRequestBehavior.AllowGet);
            }
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
