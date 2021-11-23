using System.Threading;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;

namespace Management.Interface
{
    /// <summary>
    /// Interface for handling three api clients 
    /// </summary>
    public interface IWeightedClient
    {
        /// <summary>
        /// Calculate score of cities
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> with a status code.</returns>
        Task<(State state, double score)> CalculateScoreForStateAsync(
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken);
    }
}
