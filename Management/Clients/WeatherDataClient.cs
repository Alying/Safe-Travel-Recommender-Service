using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;
using Management.Interface;

namespace Management.Clients
{
    /// <summary>
    /// Representation of the client for the weather API.
    /// </summary>
    public class WeatherDataClient : IWeatherDataClient
    {
        /// <summary>
        /// Calculate score of cities
        /// </summary>
        /// <param name="city">city of interest eg. NYC.</param>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> with a status code.</returns>
        public Task<Dictionary<City, int>> CalculateScoresAsync(IEnumerable<City> city, State state, CountryCode countryCode, CancellationToken cancellationToken)
            => Task.FromResult(new Dictionary<City, int>());

        /// <summary>
        /// Get supported cities of a state
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> with a status code.</returns>
        public Task<IEnumerable<City>> GetDefaultCitiesAsync(State state, CountryCode countryCode, CancellationToken cancellationToken)
            => Task.FromResult(Enumerable.Empty<City>());
    }
}
