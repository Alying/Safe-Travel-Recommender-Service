// <copyright file="RecommendationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Service.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Management.Ports;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for the safe-travel recommendations and safe-travel information for this
    /// safe-travel service.
    /// </summary>
    [Route("api/recommendation")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly RecommendationPort _recommendationPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationController"/> class.
        /// </summary>
        /// <param name="recommendationPort">port for the recommendation endpoints.</param>
        public RecommendationController(RecommendationPort recommendationPort)
        {
            this._recommendationPort = recommendationPort ?? throw new ArgumentNullException(nameof(recommendationPort));
        }

        /// <summary>
        /// Intended to get the top-10 recommendations for safe travel for the user.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [HttpGet]
        public IActionResult GetTopRecommendations()
        {
            try
            {
                return this.Ok();
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Intended to get the basic travel information about a specific country,
        /// which can include COVID-19, weather, and air quality information for the user.
        /// </summary>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <returns>status code.</returns>
        [HttpGet]
        [Route("country/{countryCode}")]
        public IActionResult GetRecommendationByCountryCode([FromRoute] string countryCode)
        {
            try
            {
                return this.Ok();
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Intended to get the basic travel information about a specific state in a country,
        /// which can include COVID-19, weather, and air quality information for the user.
        /// </summary>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="state">state code eg. "NY".</param>
        /// <returns>status code.</returns>
        [HttpGet]
        [Route("country/{countryCode}/state/{state}")]
        public IActionResult GetRecommendationByCountryCodeAndState(
            [FromRoute] string countryCode,
            [FromRoute] string state)
        {
            try
            {
                return this.Ok();
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }
    }
}
