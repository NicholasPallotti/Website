using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NicholasPallotti.Models;
using NicholasPallotti.Helpers;

namespace NicholasPallotti.Controllers
{
    public class ShippingController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id)
        {
            
            PackageViewModel model = new PackageViewModel();

            if (string.IsNullOrEmpty(id))
            {

                model.shippingType = "package";

                model.package.Sender.FirstName = "Jane";
                model.package.Sender.LastName = "Seymour";
                model.package.Sender.Address = "2345 First Avenue";
                model.package.Sender.City = "Hollywood";
                model.package.Sender.State = "CA";
                model.package.Sender.Zip = "90034";

                model.package.Recipient.FirstName = "Steve";
                model.package.Recipient.LastName = "Jones";
                model.package.Recipient.Address = "2345 West Broadway";
                model.package.Recipient.City = "Boulder";
                model.package.Recipient.State = "CO";
                model.package.Recipient.Zip = "80524";

            }
            else
            {
                //edit mode

                model.package = PackageDataAccess.GetPackage(id);
         
                string packageType = Package.getType(model.package);

                model.shippingType = packageType;
            }
                return View(model);
        }

        [HttpPost]
        public ActionResult Index(PackageViewModel model, string button)
        {
            //get the list of packages from session
          
            List<Package> packages = PackageDataAccess.GetPackageList();

            switch (model.shippingType)
            {
                case "Standard":
                    Package package = new Package();
                    LoadPackageFromForm(package, model);
                    model.package = package;
                    if (button == "Save")
                    {
                        UpsertPackage(package);
                    }
                    break;

                case "Two Day":
                    TwoDayPackage twoDayPackage = new TwoDayPackage(5);
                    LoadPackageFromForm(twoDayPackage, model);
                    model.package = twoDayPackage;
                    if (button == "Save")
                    {
                        UpsertPackage(twoDayPackage);
                    }
                    break;
                case "Overnight":
                    OvernightPackage overnightPackage = new OvernightPackage(10);
                    LoadPackageFromForm(overnightPackage, model);
                    model.package = overnightPackage;
                    if (button == "Save")
                    {
                        UpsertPackage(overnightPackage);
                    }
                    break;
            }
            //if they used save button, redirect to list page
            if (button == "Save")
            {
                //Go back to the PersonList page
                return RedirectToAction("ShippingList", "Shipping");
            }
            else
            {
                return View(model);
            }
        }

        private void LoadPackageFromForm(Package myPackage, PackageViewModel model)
        {
            myPackage.weight = model.package.weight;
            myPackage.costPerOunce = model.package.costPerOunce;
            
            myPackage.uniqueId = model.package.uniqueId;
            myPackage.Sender.UniqueId = model.package.Sender.UniqueId;
            myPackage.Sender.UniqueId = model.package.Sender.UniqueId;

            //set sender info
            myPackage.Sender.FirstName = model.package.Sender.FirstName;
            myPackage.Sender.LastName = model.package.Sender.LastName;
            myPackage.Sender.Address = model.package.Sender.Address;
            myPackage.Sender.City = model.package.Sender.City;
            myPackage.Sender.State = model.package.Sender.State;
            myPackage.Sender.Zip = model.package.Sender.Zip;

            //set recipient info
            myPackage.Recipient.FirstName = model.package.Recipient.FirstName;
            myPackage.Recipient.LastName = model.package.Recipient.LastName;
            myPackage.Recipient.Address = model.package.Recipient.Address;
            myPackage.Recipient.City = model.package.Recipient.City;
            myPackage.Recipient.State = model.package.Recipient.State;
            myPackage.Recipient.Zip = model.package.Recipient.Zip;

        }

        public ActionResult ShippingList(String sortDirection)
        {
            PackageListViewModel model = new PackageListViewModel();

            List<Package> packages = PackageDataAccess.GetPackageList();

            if (string.IsNullOrEmpty(sortDirection) == false)
            {
                if (sortDirection == "asc")
                {
                    packages.Sort();
                }
                else
                {
                    packages = packages.OrderByDescending(x => x.Sender.LastName).ToList();
                }

                //put the sorted list back in session
                FileHelper.savePackageListToJsonFile(packages);
            }

            model.Packages.AddRange(packages);

            return View(model);
        }
        public ActionResult Delete(string id)
        {

            Package package = PackageDataAccess.GetPackage(id);
            PackageDataAccess.DeletePackage(package);

            return RedirectToAction("ShippingList", "Shipping");
        }
        private void UpsertPackage(Package package)
        {
            if (String.IsNullOrEmpty(package.uniqueId))
            {
                //add mode, just add it to the list, after making sure it has a new unique id
                package.uniqueId = Guid.NewGuid().ToString();
              
                PackageDataAccess.InsertPackage(package);
            }
            else
            {
                PackageDataAccess.UpdatePackage(package);
            }
        }

    }
    
}