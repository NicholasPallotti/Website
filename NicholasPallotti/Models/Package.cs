using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class Package
    {

        //weight get/set
        [Range(1, 100, ErrorMessage = "package must be atleast 1 ounce, and no more than 100 ounces")]
        private decimal _weight = 1;

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
        private decimal _costPerOunce = 1;

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
        private decimal _totalCost;

        public virtual decimal totalCost
        {
            get
            {
                return _weight * _costPerOunce;
            }
        }

        //recipient Person variable
        private Person _Recipient = new Person();

        public Person Recipient
        {
            get
            {
                return _Recipient;
            }
        }

        //sender Person variable
        private Person _Sender = new Person();

        public Person Sender
        {
            get
            {
                return _Sender;
            }
        }
        public Package()
        {
           
        }

        //public virtual void calculateCost()
        //{
        //    totalCost = (decimal)weight * CostPerOunce;
        //}
    }

    
}