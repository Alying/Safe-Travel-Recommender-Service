using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
using Management.DomainModels;
using Management.Enum;

namespace Management.Interface
{
    /// <summary>
    /// Interface for air quality data client functions
    /// </summary>
    public interface IAirQualityDataClient : IWeightedClient
    {
        /// <summary>
        /// Retrieve city data from air quality api
        /// </summary>
        /// <param name="city">city of interest eg. NYC.</param>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        Task<AirQualityCityResponse> GetCityAirQualityDataAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken);
    }
}
