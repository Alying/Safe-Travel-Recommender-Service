using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
using Management.DomainModels;

namespace Management.Interface
{
    public interface ICovidDataClient : IWeightedClient
    {
        /// <summary>
        /// Retrieve city data from covid api
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        Task<CovidStateResponse> GetStateCovidDataAsync(
            State state,
            CancellationToken cancellationToken);
    }
}
