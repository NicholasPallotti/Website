using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class TwoDayPackage : Package
    {
        [Range(5, 100, ErrorMessage = "flat fee must be at least $5")]
        private decimal _fee;

        public decimal fee
        {
            get
            {
                return _fee;
            }
            set
            {
                _fee = value;
            }
        }

        public TwoDayPackage(decimal Fee)
        {
            fee = Fee;
        }

        private decimal _totalCost;

        public override decimal totalCost
        {
            get
            {
                return base.totalCost + _fee;
            }
        }

        //public override void calculateCost()
        //{
        //    totalCost = Fee + ((decimal)weight * CostPerOunce);
        //}

    }


}