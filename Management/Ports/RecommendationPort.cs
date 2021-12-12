using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Interface;
using Management.Mapping;

namespace Management.Ports
{
    /// <summary>
    /// Recommendation ports for async task.
    /// </summary>
    public class RecommendationPort
    {
        private readonly IDecisionEngine _decisionEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationPort"/> class.
        /// </summary>
        /// <param name="decisionEngine">decision engine for recommendation port.</param>
        public RecommendationPort(IDecisionEngine decisionEngine)
        {
            _decisionEngine = decisionEngine ?? throw new ArgumentNullException(nameof(decisionEngine));
        }

        /// <summary>
        /// Get top 10 recommended locations for a specific country
        /// </summary>
        /// <param name="country">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<IEnumerable<ApiModels.Recommendation>> GetDefaultRecommendationAsync(
            string country,
            CancellationToken cancellationToken)
        {
            var validatedCountry = CountryStateValidator.ValidateCountry(country);

            var result = await _decisionEngine.GetDefaultCountryRecommendationAsync(validatedCountry, cancellationToken);

            return result.Select(res => DomainToApiMapper.ToApi(res));
        }

        /// <summary>
        /// Gets the specific location's information. 
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="state">The state in country.</param>
        /// /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The state's information.</returns>
        public async Task<ApiModels.Recommendation> GetStateInfoAsync(string countryCode, string state, CancellationToken cancellationToken)
        {
            var (validatedCountry, validatedState) = CountryStateValidator.ValidateCountryState(countryCode, state);

            var result = await _decisionEngine.GetStateInfoAsync(validatedCountry, validatedState, cancellationToken);

            return DomainToApiMapper.ToApi(result);
        }
    }
}
