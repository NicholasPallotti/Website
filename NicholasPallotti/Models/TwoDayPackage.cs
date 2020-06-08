using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using System;

namespace NicholasPallotti.Models
{
    [Serializable]
    public class TwoDayPackage : Package
    {
        [Range(5, 100, ErrorMessage = "flat fee must be at least $5")]
        private decimal _twoDayFee = 5;

        public decimal twoDayFee
        {
            get
            {
                return _twoDayFee;
            }
            set
            {
                _twoDayFee = value;
            }
        }

        public TwoDayPackage(decimal Fee)
        {
            this.twoDayFee = Fee;
        }

        public TwoDayPackage()
        {
            this.twoDayFee = 5;
        }
        public override decimal totalCost
        {
            get
            {
                return base.totalCost + _twoDayFee;
            }
        }

    }


}