using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;

namespace NicholasPallotti
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "errors.txt"); ;
            StreamWriter writer = null;

            try
            {
                if (!System.IO.File.Exists(path))
                {
                    writer = new StreamWriter(path);
                }
                else
                {
                    writer = System.IO.File.AppendText(path);
                }
                
                writer.WriteLine(ex + "\n" + "Logged at: " + DateTime.Now);

                writer.WriteLine();
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
