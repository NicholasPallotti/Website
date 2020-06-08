using System.Collections.Generic;
using System.Web.Mvc;

namespace NicholasPallotti.Models
{
    public class PersonViewModel
    {
        public Person mailing { get; set; }

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
        public PersonViewModel()
        {
            mailing = new Person();
        }
    }
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