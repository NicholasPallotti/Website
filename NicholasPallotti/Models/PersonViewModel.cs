using System.Collections.Generic;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class PersonViewModel
    {
        //example of composition, a seperate class for product will be more re-usable 
        //than having it in the view model
        public Person mailingFrom { get; set; }
        public Person mailingTo { get; set; }
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

        //constructor creates instance of Product to prevent null reference exceptions
        public PersonViewModel()
        {
            mailingFrom = new Person();
            mailingTo = new Person();
        }
    }

    //this could be in it's own file
    public class State
    {
        public string state { get; set; }
        public string Id { get; set; }

        //empty constructor
        public State()
        {

        }

        //constructor that accepts parameters for name and id
        public State(string _state, string id)
        {
            state = _state;
            Id = id;
        }
    }
}