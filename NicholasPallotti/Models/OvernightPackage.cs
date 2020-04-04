using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class OvernightPackage : Package
    {
        [Range(5, 100, ErrorMessage = "flat fee must be at least $5")]
        private decimal _feePerOunce;

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

        private decimal _totalCost;

        public override decimal totalCost
        {
            get
            {
                return base.totalCost + _feePerOunce;
            }
        }

        //public override void calculateCost()
        //{
        //    totalCost = ((FeePerOunce + CostPerOunce) * (decimal)weight);
        //}


    }
}