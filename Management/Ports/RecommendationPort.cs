// <copyright file="RecommendationPort.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;

namespace Management.Ports
{
    /// <summary>
    /// Recommendation ports for async task.
    /// </summary>
    public class RecommendationPort
    {
        private readonly IDecisionEngine _decisionEngine;

        public RecommendationPort(IDecisionEngine decisionEngine)
        {
            _decisionEngine = decisionEngine ?? throw new ArgumentNullException(nameof(decisionEngine));
        }

        /// <summary>
        /// Gets the specific location's information. 
        /// </summary>
        /// <param name="location">The country and state the user inquired.</param>
        /// <param name="userId">The user's unique id.</param>
        /// <returns>The state's information.</returns>
        public async Task<Recommendation> GetLocationInfoAsync(Location location, UserId userId)

            => await _decisionEngine.GetSpecificLocationInfoAsync(location, userId);
    }
}
