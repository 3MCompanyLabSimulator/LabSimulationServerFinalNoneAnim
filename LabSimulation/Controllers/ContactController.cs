using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using LabSimulation.Models;

namespace LabSimulation.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LabSimulation.Models.gmail model)
        {
            
            MailMessage mailMessage = new MailMessage(model.From, "3mcomany@gmail.com");
            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;
            
            mailMessage.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential networkCredential = new NetworkCredential("3mcomany@gmail.com", "<3MCom@ny>");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCredential;
            smtp.Send(mailMessage);

            TempData["SendSuccess"] = "Mail has been send successfully";

            return View();
        }

    }
}