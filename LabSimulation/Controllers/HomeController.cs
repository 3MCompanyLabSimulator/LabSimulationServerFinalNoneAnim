using LabSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabSimulation.Controllers
{
    public class HomeController : Controller
    {
        private LabSimulationDBEntities db = new LabSimulationDBEntities();
        public ActionResult Index()
        {
            var getEducationalLeve_CountExperience = GetEducationalLeve_CountExperience();
            var getExperienceInfo = GetExperienceInfo();

            HomeIndexVM model = new HomeIndexVM();
            model.EducationalLeve_CountExperience = getEducationalLeve_CountExperience;
            model.ExperienceInfo = getExperienceInfo;

            return View(model);
        }

        private List<View_EducationalLeve_CountExperience> GetEducationalLeve_CountExperience()
        {
            return db.View_EducationalLeve_CountExperience.ToList();
        }

        private List<View_ExperienceInfo> GetExperienceInfo()
        {
            return db.View_ExperienceInfo.ToList();
        }


        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("Login");
            cookie.Expires = DateTime.Now.AddDays(-7d);
            Response.Cookies.Add(cookie);


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }



    }
}