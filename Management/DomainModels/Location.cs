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
            Country = country ?? throw new ArgumentNullException(nameof(country));
            State = state ?? throw new ArgumentNullException(nameof(state));

            if(!isLocationValid(country, state))
            {
                throw new ArgumentException($"Invalid country state combination: {country.Value}, {state.Value}");
            }
        }

        private bool isLocationValid(Country country, State state)
        {
            // TODO: @mli: implement this to actually check country & state code.
            Console.WriteLine($"Country: {Country.Value}, State: {State.Value}");
            return true;
        }
    }
}
