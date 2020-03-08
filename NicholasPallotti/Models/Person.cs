using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class Person
    {

        //class variables used for the address
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string StateId { get; set; }
        [Required]
        public string Zip { get; set; }


        [AllowHtml]
        public string HtmlFormattedFormData
        {
            get
            {
                //string to format the mailing label
                StringBuilder label = new StringBuilder();

                //first line of mailing label is 'FirstName, LastName'
                label.Append(FirstName);
                label.Append(", ");
                label.Append(LastName);
                label.Append("<br/>");

                //second line of mailing address is  'Address'
                label.Append(Address);
                label.Append("<br/>");

                //last line of mailing address is 'city, State, Zip"
                label.Append(City);
                label.Append(", ");
                label.Append(State);
                label.Append(", ");
                label.Append(Zip);
                label.Append("<br/>");   

                return label.ToString();
            }
        }
    }
}