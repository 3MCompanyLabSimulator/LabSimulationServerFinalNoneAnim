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
    public class StudentsController : Controller
    {
        private LabSimulationDBEntities db = new LabSimulationDBEntities();

        // GET: Students
        public ActionResult Index()
        {
            if (Request.Cookies["Login"]["UserType"] == "School")
            {
                int id = int.Parse(Request.Cookies["Login"]["UserID"]);
                var users = db.Users.Where(m=>m.SchoolID == id);
                return View(users.ToList());
            }
            else
            {
                return RedirectToAction("Index", "PageNotFound");
            }
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
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
            return PartialView(user);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            if (Request.Cookies["Login"] != null)
            {
                if (Request.Cookies["Login"]["UserType"] == "School")
                {
                    int id = int.Parse(Request.Cookies["Login"]["UserID"]);
                    if (Request.Cookies["Login"]["IsPremium"] == "False" && db.Users.Where(m => m.SchoolID == id).Count() >= 15)
                    {
                        TempData["MaxStudents"] = "Sorry, You can't add more than 15 student";
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Index", "PageNotFound");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,SchoolName,UserName,Email,Password,Image,IsConfirm,IsPremiumAccount,SchoolID,UserTypeID")] User user, HttpPostedFileBase httpPostedFileBase)
        {
            
            if (ModelState.IsValid)
            {
                int id = int.Parse(Request.Cookies["Login"]["UserID"]);
                if (Request.Cookies["Login"]["IsPremium"] == "False" && db.Users.Where(m => m.SchoolID == id).Count() >=15)
                {
                    TempData["MaxStudents"] = "Sorry, You can't add more than 15 student";
                    return View(user);
                }
                else
                {
                    if (httpPostedFileBase != null)
                    {
                        user.Image = new byte[httpPostedFileBase.ContentLength];
                        httpPostedFileBase.InputStream.Read(user.Image, 0, httpPostedFileBase.ContentLength);
                    }
                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["AnotherStudent"] = "Do you want to add another student?";
                    return View();
                }


            }

            return View(user);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.Cookies["Login"] != null)
            {

                if (Request.Cookies["Login"]["UserType"] == "School")
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
                else
                {
                    return RedirectToAction("Index", "PageNotFound");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,SchoolName,UserName,Email,Password,Image,IsConfirm,IsPremiumAccount,SchoolID,UserTypeID")] User user, HttpPostedFileBase httpPostedFileBase)
        {
            if (ModelState.IsValid)
            {
                byte[] img = db.Users.Where(s => s.ID == user.ID).Select(s=>s.Image).SingleOrDefault();
                if (httpPostedFileBase != null && img==null)
                {
                    user.Image = new byte[httpPostedFileBase.ContentLength];
                    httpPostedFileBase.InputStream.Read(user.Image, 0, httpPostedFileBase.ContentLength);
                }
                else if(httpPostedFileBase == null && img != null)
                {
                    user.Image = img;
                }
                else if(httpPostedFileBase != null && img != null)
                {
                    user.Image = new byte[httpPostedFileBase.ContentLength];
                    httpPostedFileBase.InputStream.Read(user.Image, 0, httpPostedFileBase.ContentLength);
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypeAccounts, "ID", "Name", user.UserTypeID);
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
