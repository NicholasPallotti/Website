using System.Text;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class Employee
    {

        //class variables used for the address
        public string Name { get; set; }
        public int EmployeeNumber { get; set; }
        public decimal Hours { get; set; }
        public decimal Rate { get; set; }
        public decimal Wage  { get; set; }

        [AllowHtml]
        public string HtmlFormattedFormData
        {
            get
            {
                //string to format the mailing label
                StringBuilder label = new StringBuilder();

                if(Hours > 40)
                {
                    Hours -= 40;
                    Wage = (Rate * (decimal)1.5) * (Hours);
                    Wage = Wage + (Rate * (decimal)40);
                }
                else
                {
                    Wage = Rate * Hours;
                }
            
                return Wage.ToString();
            }
        }
    }
}