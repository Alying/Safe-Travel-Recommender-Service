using System;
using Management.Enum;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a location, including country and state.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets the country 
        /// </summary>
        public CountryCode CountryCode { get; }

        /// <summary>
        /// Gets the state
        /// </summary>
        public State State { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="state">state code eg. "NY".</param>
        public Location(CountryCode countryCode, State state)
        {
            CountryCode = countryCode;
            State = state ?? throw new ArgumentNullException(nameof(state));
        }
    }
}
