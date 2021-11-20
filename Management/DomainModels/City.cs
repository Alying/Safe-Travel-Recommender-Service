using Common;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of city data
    /// </summary>
    public class City : TaggedString<City>
    {
        private City(string city)
                : base(city)
        {
        }

        /// <summary>
        /// Wrap city data.
        /// </summary>
        /// <param name="city">city name.</param>
        /// <returns>Wrapped city data.</returns>
        public static City Wrap(string city) => new City(city);
    }
}
