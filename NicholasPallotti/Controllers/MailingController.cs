using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NicholasPallotti.Models;

namespace NicholasPallotti.Controllers
{
    public class MailingController : Controller
    {
        // GET: Mailing
        public ActionResult Index()
        {

            PersonViewModel fillerData = new PersonViewModel();
            fillerData.mailing.FirstName = "John";
            fillerData.mailing.LastName = "Smith";
            fillerData.mailing.Address = "123 Glennsdale dr.";
            fillerData.mailing.City = "Montgomery";
            fillerData.mailing.State = "Alabama";
            fillerData.mailing.Zip = "80808";

            return View(fillerData);
        }

        
        [HttpPost]
        public ActionResult Index(PersonViewModel model)
        {
            //return the data in the web page
            return View(model);
        }
    }
}
