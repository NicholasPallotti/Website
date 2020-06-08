using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using System;

namespace NicholasPallotti.Models
{
    [Serializable]
    public class OvernightPackage : Package
    {

        [Range(5, 100, ErrorMessage = "fee per ounce must be at least $5")]
        private decimal _feePerOunce = 10;

        public decimal feePerOunce
        {
            get
            {
                return _feePerOunce;
            }
            set
            {
                _feePerOunce = value;
            }
        }

        public OvernightPackage(decimal Fee)
        {
            feePerOunce = Fee;
        }
        public OvernightPackage()
        {
            feePerOunce = 10;
        }

        public override decimal totalCost
        {
            get
            {
                return base.totalCost + (_feePerOunce * base.weight);
            }
        }
       


    }
}