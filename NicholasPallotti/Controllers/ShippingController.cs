using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NicholasPallotti.Models;

namespace NicholasPallotti.Controllers
{
    public class ShippingController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            PackageViewModel fillerData = new PackageViewModel();
            fillerData.package.Recipient.FirstName = "John";
            fillerData.package.Recipient.LastName = "Smith";
            fillerData.package.Recipient.Address = "123 Glennsdale dr.";
            fillerData.package.Recipient.City = "Montgomery";
            fillerData.package.Recipient.State = "Alabama";
            fillerData.package.Recipient.Zip = "80808";

            fillerData.package.Sender.FirstName = "Mary";
            fillerData.package.Sender.LastName = "Sue";
            fillerData.package.Sender.Address = "456 Goober Ct.";
            fillerData.package.Sender.City = "Denver";
            fillerData.package.Sender.State = "Colorado";
            fillerData.package.Sender.Zip = "90909";

            return View(fillerData);
        }

        [HttpPost]
        public ActionResult Index(PackageViewModel model)
        {
            //return the data in the web page
            return View(model);
        }
    }
}