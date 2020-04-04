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

            switch (model.shippingType)
            {
                case "Standard":
                    Package package = new Package();
                    LoadPackageFromForm(package, model);
                    model.package = package;
                    break;
                case "Two Day":
                    TwoDayPackage twoDayPackage = new TwoDayPackage(5);
                    LoadPackageFromForm(twoDayPackage, model);
                    model.package = twoDayPackage;
                    break;
                case "Overnight":
                    OvernightPackage overnightPackage = new OvernightPackage(10);
                    LoadPackageFromForm(overnightPackage, model);
                    model.package = overnightPackage;
                    break;
            }
            //return the data in the web page
            return View(model);
        }

        private void LoadPackageFromForm(Package myPackage, PackageViewModel model)
        {
            myPackage.weight = model.package.weight;
            myPackage.costPerOunce = model.package.costPerOunce;

            //set sender info
            myPackage.Sender.FirstName = model.package.Sender.FirstName;
            myPackage.Sender.LastName = model.package.Sender.LastName;
            myPackage.Sender.Address = model.package.Sender.Address;
            myPackage.Sender.City = model.package.Sender.City;
            myPackage.Sender.State = model.package.Sender.State;
            myPackage.Sender.Zip = model.package.Sender.Zip;

            //set recipient info
            myPackage.Recipient.FirstName = model.package.Sender.FirstName;
            myPackage.Recipient.LastName = model.package.Sender.LastName;
            myPackage.Recipient.Address = model.package.Sender.Address;
            myPackage.Recipient.City = model.package.Sender.City;
            myPackage.Recipient.State = model.package.Sender.State;
            myPackage.Recipient.Zip = model.package.Sender.Zip;

           // myPackage.calculateCost();
        }
    }
}