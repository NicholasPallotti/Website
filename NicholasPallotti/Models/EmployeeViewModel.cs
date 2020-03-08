using System.Collections.Generic;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class EmployeeViewModel
    {
        //example of composition, a seperate class for product will be more re-usable 
        //than having it in the view model
        public Employee employee { get; set; }

        //constructor creates instance of Product to prevent null reference exceptions
        public EmployeeViewModel()
        {
            employee = new Employee();
        }
    }
}