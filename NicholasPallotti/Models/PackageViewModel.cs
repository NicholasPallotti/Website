using System.Collections.Generic;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class PackageViewModel
    {
        //example of composition, a seperate class for product will be more re-usable 
        //than having it in the view model
        public Package package { get; set; }
  

        //This is the lisstatest of Manufactures
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

        public List<ListItem> ShippingType
        {
            get
            {
                List<ListItem> shipping = new List<ListItem>();

                shipping.Add(new ListItem("Standard"));
                shipping.Add(new ListItem("Two Day"));
                shipping.Add(new ListItem("Overnight"));

                return shipping;

            }
        }

        public class ListItem
        {
            public string Shipping { get; set; }

            public ListItem(string shipping)
            {
                Shipping = shipping;
            }
        }

        //constructor creates instance of Product to prevent null reference exceptions
        public PackageViewModel()
        {
            package = new Package();
        }
    }
}