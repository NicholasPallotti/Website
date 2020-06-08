using System.Collections.Generic;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class PackageViewModel
    {
        public Package package { get; set; }

        public string shippingType { get; set; }

        //This is the list of states
        public SelectList StatesList
        {
            get
            {
                //create a collection of states
                List<State> states = new List<State>();
                //Add some items to the list
                states.Add(new State("Alabama", "AL"));
                states.Add(new State("Alaska", "AK"));
                states.Add(new State("Arizona", "AZ"));
                states.Add(new State("Arkansas", "Ar"));
                states.Add(new State("California", "CA"));
                states.Add(new State("Colorado", "CO"));

                //create a new SelectList passing in the list of sites, Id as the value field, 
                //Name as the description field
                return new SelectList(states, "State", "ID");
            }
        }

        public List<ListItem> ShippingTypeList
        {
            get
            {
                List<ListItem> shipping = new List<ListItem>();

                shipping.Add(new ListItem("Standard", "Standard"));
                shipping.Add(new ListItem("Two Day", "Two Day"));
                shipping.Add(new ListItem("Overnight", "Overnight"));

                return shipping;

            }
        }

        public class ListItem
        {
            public string Shipping { get; set; }
            public string Description { get; set; }
            public ListItem(string description, string shipping)
            {
                Shipping = shipping;
                Description = description;
            }
        }
        public PackageViewModel()
        {
            package = new Package();
            shippingType = "standard";

            package.Sender.FirstName = "Jane";
            package.Sender.LastName = "Seymour";
            package.Sender.Address = "2345 First Avenue";
            package.Sender.City = "Hollywood";
            package.Sender.State = "CA";
            package.Sender.Zip = "90034";

            package.Recipient.FirstName = "Steve";
            package.Recipient.LastName = "Jones";
            package.Recipient.Address = "2345 West Broadway";
            package.Recipient.City = "Boulder";
            package.Recipient.State = "CO";
            package.Recipient.Zip = "80524";
        }

    }

}