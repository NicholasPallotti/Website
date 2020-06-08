using System.Collections.Generic;
using System.Web;

namespace NicholasPallotti.Models
{
    public class SessionHelper
    {
        public static List<Package> GetPackagesFromSession()
        {
            List<Package> Packages = null;

            //Look in Session for a list of Packages with the key of "Packages"
            if (HttpContext.Current.Session["Packages"] != null)
            {
                //Session is of datatype object, must case to a list of Package
                Packages = HttpContext.Current.Session["Packages"] as List<Package>;
            }
            else
            {
                //if it's not found, create a new empty list
                Packages = new List<Package>();
            }

            return Packages;
        }

        public static void SetPackagesInSession(List<Package> Packages)
        {
            HttpContext.Current.Session["Packages"] = Packages;
        }
    }
}