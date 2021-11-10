using System;
using System.Collections.Generic;
using System.Text;

namespace Management.DomainModels
{
    public class Location
    {
        public Country Country { get; }
        public State State { get; }

        public Location(Country country, State state)
        {
            if(!isLocationValid(country, state))
            {
                throw new ArgumentException("Invalid country state combination: " + country.Value + ", " + state.Value);
            }
            Country = country;
            State = state;
        }

        private bool isLocationValid(Country country, State state)
        {
            // TODO: @mli: implement this to actually check country & state code.
            Console.WriteLine("Country: {0}; State: {1}", country.Value, state.Value);
            return true;
        }
    }
}
