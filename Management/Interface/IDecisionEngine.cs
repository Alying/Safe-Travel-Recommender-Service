using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;

namespace Management.Interface
{
    /// <summary>
    /// Interface for recommendation's decision engine.
    /// </summary>
    public interface IDecisionEngine
    {
        /// <summary>
        /// Get top 10 recommended locations for a specific country
        /// </summary>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The weighted score.</returns>
        Task<IEnumerable<Recommendation>> GetDefaultCountryRecommendationAsync(
            CountryCode countryCode,
            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specific location's information.
        /// </summary>
        /// <param name="countryCode">The country and state the user inquired.</param>
        /// <param name="state">The user's unique id.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The state's information.</returns>
        Task<Recommendation> GetStateInfoAsync(CountryCode countryCode, State state, CancellationToken cancellationToken);
    }
}
