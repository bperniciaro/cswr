using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cswr.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Cswr.Web.Models.ViewModels.ContactViewModel;

namespace Cswr.Web.Controllers
{
    public class ContactController : Controller
    {

        [Route("/contact", Name = "Contact")]
        public IActionResult Index()
        {
            var contactForm = new ContactViewModel();

            // Contact Type 1
            contactForm.ContactInquiryTypes = new List<ContactInquiryType>()
            {
                ContactInquiryType.Common,
                ContactInquiryType.Question,
                ContactInquiryType.Error,
                ContactInquiryType.Advertising,
                ContactInquiryType.Other
            };

            return View(contactForm); 
        }



        [HttpPost]
        public IActionResult SendEmail(ContactViewModel contactFormInfo)
        {
            bool success = false;

            if(success)
            {
                TempData["Success"] = "Sucess Sending Email";
                return RedirectToAction("index");
            }
            else
            {
                TempData["Error"] = "Error Sending Email";
                return View("Index", contactFormInfo);
            }

        }
    }
}
