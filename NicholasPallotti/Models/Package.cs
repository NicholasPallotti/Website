using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class Package
    {
        [Range(1, 100, ErrorMessage = "package must be atleast 1 ounce, and no more than 100 ounces")]
        public double weight { get; set; }

        [Range(1, 1000000, ErrorMessage = "cost per ounce must me at least $1.00")]
        public decimal CostPerOunce { get; set; }
        [Required]

        public Person Recipient { get; set; }

        public Person Sender { get; set; }
        public Package()
        {
            Recipient = new Person();
            Sender = new Person();
        }
    }

    
}