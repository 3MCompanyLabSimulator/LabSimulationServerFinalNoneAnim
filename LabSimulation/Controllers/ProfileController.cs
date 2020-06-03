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
    public class ProfileController : Controller
    {
        private LabSimulationDBEntities db = new LabSimulationDBEntities();

        [HttpPost]
        public JsonResult CheckIsOldPassword(string OldPassword, int Id)
        {
            var searchData = db.Users.Where(x => x.ID == Id && x.Password==OldPassword).SingleOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        // GET: Profile/Index/5
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "PageNotFound");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index", "PageNotFound");
            }
            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploudPhoto([Bind(Include = "ID,Image")] User user, HttpPostedFileBase httpPostedFileBase)
        {
            user.Image = new byte[httpPostedFileBase.ContentLength];
            httpPostedFileBase.InputStream.Read(user.Image, 0, httpPostedFileBase.ContentLength);
            user.ID = int.Parse(Request.Cookies["Login"]["UserID"]);
            List<object> parameters = new List<object>();
            parameters.Add(user.Image);
            parameters.Add(user.ID);
            object[] objectarray = parameters.ToArray();

            db.Database.ExecuteSqlCommand("update Users set Image=@p0 where ID=@p1", objectarray);

            return RedirectToAction("Index", new { id = user.ID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPersonalInfo([Bind(Include = "ID,FirstName,LastName,UserName")] User user)
        {

            user.ID = int.Parse(Request.Cookies["Login"]["UserID"]);
            List<object> parameters = new List<object>();
            parameters.Add(user.FirstName);
            parameters.Add(user.LastName);
            parameters.Add(user.UserName);
            parameters.Add(user.ID);
            object[] objectarray = parameters.ToArray();

            db.Database.ExecuteSqlCommand("update Users set FirstName=@p0,LastName=@p1,UserName=@p2 where ID=@p3", objectarray);
            var userAccount = db.View_User.Where(m => m.ID == user.ID).ToList().FirstOrDefault();
            UsersController.cookie["FullName"] = userAccount.FullName;
            Response.Cookies.Add(UsersController.cookie);


            return RedirectToAction("Index", new { id = user.ID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSecurityInfo([Bind(Include = "ID,Password")] User user)
        {
            user.ID = int.Parse(Request.Cookies["Login"]["UserID"]);
            List<object> parameters = new List<object>();
            parameters.Add(user.Password);
            parameters.Add(user.ID);
            object[] objectarray = parameters.ToArray();

            db.Database.ExecuteSqlCommand("update Users set Password=@p0 where ID=@p1", objectarray);

            return RedirectToAction("Index", new { id = user.ID });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePricePlan([Bind(Include = "ID,IsPremiumAccount")] User user)
        {
            user.ID = int.Parse(Request.Cookies["Login"]["UserID"]);
            List<object> parameters = new List<object>();
            parameters.Add(user.IsPremiumAccount);
            parameters.Add(user.ID);
            object[] objectarray = parameters.ToArray();

            db.Database.ExecuteSqlCommand("update Users set IsPremiumAccount=@p0 where ID=@p1", objectarray);

            if(Request.Cookies["Login"]["UserType"] =="School")
                db.Database.ExecuteSqlCommand("update Users set IsPremiumAccount=@p0 where SchoolID=@p1", objectarray);

            UsersController.cookie["IsPremium"] = user.IsPremiumAccount.ToString();
            Response.Cookies.Add(UsersController.cookie);


            return RedirectToAction("Index", new { id = user.ID });

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
