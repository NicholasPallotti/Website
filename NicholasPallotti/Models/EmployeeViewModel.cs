using System.Collections.Generic;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class EmployeeViewModel
    {
        public Employee employee { get; set; }

        //constructor creates instance of Product to prevent null reference exceptions
        public EmployeeViewModel()
        {
            employee = new Employee();
        }
    }
}