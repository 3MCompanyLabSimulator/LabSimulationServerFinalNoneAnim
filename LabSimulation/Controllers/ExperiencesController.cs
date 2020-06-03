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
    public class ExperiencesController : Controller
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


        // GET: Experiences/Details/5

        public ActionResult Details(int? id, int? subjectID, int? educationalYearID, int? educationalLevelID)
        {
            if (id == null || subjectID == null || educationalYearID == null || educationalLevelID == null)
            {
                return RedirectToAction("Index", "PageNotFound");
            }

            HomeIndexVM model = new HomeIndexVM();
            model.comments = GetComments(id, subjectID, educationalYearID, educationalLevelID);
            model.ExperienceDetails = GetExperienceInfo(id, subjectID, educationalYearID, educationalLevelID);

            if (model.ExperienceDetails == null)
            {
                return RedirectToAction("Index", "PageNotFound");
            }
        
            if (Request.Cookies["Login"] == null)
            {
                if (model.ExperienceDetails.Type == "All visitors")
                {
                    return View(model);
                }
                else if (model.ExperienceDetails.Type == "Free accounts")
                {
                    TempData["MustLoginFree"] = "Sorry, You can't see this experiment";
                    return View(model);
                }
                else
                {
                    TempData["MustLoginPremium"] = "Sorry, You can't see this experiment";
                    return View(model);
                }
            }
            else
            {
                if (Request.Cookies["Login"]["IsPremium"] == "False")
                {
                    if (model.ExperienceDetails.Type == "Premium accounts")
                    {
                        if (Request.Cookies["Login"]["UserType"] == "Student")
                        {
                            TempData["ChangeToPremiumStudent"] = "Sorry, It's not your responsibility.";
                            return View(model);
                        }
                        else
                        {
                            TempData["ChangeToPremium"] = "Sorry, You can't see this experiment";
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
        }

        private View_ExperienceInfo GetExperienceInfo(int? id, int? subjectID, int? educationalYearID, int? educationalLevelID)
        {
            return db.View_ExperienceInfo.Where(c => c.ID == id && c.SubjectID == subjectID && c.EducationalYearID == educationalYearID && c.EducationalLevelID == educationalLevelID).SingleOrDefault();
        }
        private List<View_Comments_Users> GetComments(int? experienceID, int? subjectID, int? educationalYearID, int? educationalLevelID)
        {
            return db.View_Comments_Users.Where(c => c.ID == experienceID && c.SubjectID == subjectID && c.EducationalYearID == educationalYearID && c.EducationalLevelID == educationalLevelID).ToList();
        }


        // GET: Commintes/Create
        public ActionResult CreateComment(int? id, int? subjectID, int? educationalYearID, int? educationalLevelID)
        {
            var ExperienceDetails = GetExperienceInfo(id, subjectID, educationalYearID, educationalLevelID);
            if (ExperienceDetails == null)
            {
                return RedirectToAction("Index", "PageNotFound");
            }

            TempData["id"] = ExperienceDetails.ID;
            TempData["subjectID"] = ExperienceDetails.SubjectID;
            TempData["educationalYearID"] = ExperienceDetails.EducationalYearID;
            TempData["educationalLevelID"] = ExperienceDetails.EducationalLevelID;

            return View();
        }

        // POST: Commintes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentID,EducationalLevelID,EducationalYearID,SubjectID,ID,UserID,Comments,CommentDateTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details",new { id= comment.ID, subjectID= comment.SubjectID, educationalYearID= comment.EducationalYearID, educationalLevelID= comment.EducationalLevelID });
            }

            return View(comment);
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
