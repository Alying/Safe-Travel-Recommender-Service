// <copyright file="IDecisionEngine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
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
        /// Calculate the desired location using weighted scores from COVID-19, weather, and air quality.
        /// </summary>
        /// <param name="stateCode">state code eg. "NY".</param>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The weighted score.</returns>
        Task<Dictionary<State, double>> GetDefaultCountryRecommendationAsync(
            CountryCode countryCode,
            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specific location's information.
        /// </summary>
        /// <param name="location">The country and state the user inquired.</param>
        /// <param name="userId">The user's unique id.</param>
        /// <returns>The state's information.</returns>
        Task<Recommendation> GetSpecificLocationInfoAsync(
            Location location,
            UserId userId);
    }
}
