using Management.Enum;
using System;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a location, including country and state.
    /// </summary>
    public class Location
    {
        public CountryCode CountryCode { get; }

        public State State { get; }

        public Location(CountryCode countryCode, State state)
        {
            CountryCode = countryCode;
            State = state ?? throw new ArgumentNullException(nameof(state));
        }
    }
}
