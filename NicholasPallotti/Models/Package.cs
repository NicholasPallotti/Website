using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using System;

namespace NicholasPallotti.Models
{
    [Serializable]
    public class Package :IComparable
    {

        public string uniqueId { get; set; }

        //weight get/set
        [Range(1, 100, ErrorMessage = "package must be atleast 1 ounce, and no more than 100 ounces")]
        private decimal _weight;

        public decimal weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
            }
        }


        //cost per ounce get/set
        [Range(1, 1000000, ErrorMessage = "cost per ounce must me at least $1.00")]
        private decimal _costPerOunce;

        public decimal costPerOunce
        {
            get
            {
                return _costPerOunce;
            }
            set
            {
                _costPerOunce = value;
            }
        }


        //totalCost get

        public virtual decimal totalCost
        {
            get
            {
                return _weight * _costPerOunce;
            }
        }

        //recipient Person variable
        public Person Recipient { get; set; }


        //sender Person variable
        public Person Sender { get; set; }


        public string HtmlFormattedMailingLabel
        {
            get
            {
                StringBuilder label = new StringBuilder();
                label.Append("From:");
                label.Append("<br/>");
                label.Append(Sender.HtmlFormattedFormData);
                label.Append("<br/>");
                label.Append("To:");
                label.Append("<br/>");
                label.Append(Recipient.HtmlFormattedFormData);

                return label.ToString();
            }
        }

        public Package()
        {
            _weight = 1; //initialize to min value
            _costPerOunce = 1; //initialize to min value
            Sender = new Person();
            Recipient = new Person();
        }

        public int CompareTo(object obj)
        {
            //if the passed obj is less than the current object(this) return less than zero
            //if the passed obj is the same (for ordering purposes) return zero
            //if the passed obj is greater than the current object (this) return > 0

            //if they pass null put it at end of list
            if (obj == null)
            {
                return 1;
            }

            //Cast passed object to a Package
            Package otherPackage = obj as Package;
            if (otherPackage != null)
            {
                //Call CompareTo on the property Name (which is of type string)
                return this.Sender.LastName.CompareTo(otherPackage.Sender.LastName);
            }
            else
            {
                throw new ArgumentException("Object is not a Package");
            }
        }

        public static String getType(Package package)
        {
            string packageType = "package";
            if(package is TwoDayPackage)
            {
                packageType = "TwoDayPackage";
            }
            if(package is OvernightPackage)
            {
                packageType = "OvernightPackage";
            }
            return packageType;
        }

    }

    
}