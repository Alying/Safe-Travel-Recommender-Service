using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
using Management.DomainModels;
using Management.Enum;

namespace Management.Interface
{
    /// <summary>
    /// Interface for weather data client functions
    /// </summary>
    public interface IWeatherDataClient : IWeightedClient
    {
        /// <summary>
        /// Retrieve city data from weather api
        /// </summary>
        /// <param name="city">city of interest eg. NYC.</param>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        Task<WeatherCityResponse> GetCityWeatherDataAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken);
    }
}
