using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NicholasPallotti.Models;

namespace NicholasPallotti.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            EmployeeViewModel fillerData = new EmployeeViewModel();
            fillerData.employee.Name = "John";
            fillerData.employee.EmployeeNumber = 1234;
            fillerData.employee.Hours = 10;
            fillerData.employee.Rate = 1;
            fillerData.employee.Wage = 10;

            return View(fillerData);
        }
        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {
            return View(model);
        }

       
    }
}
