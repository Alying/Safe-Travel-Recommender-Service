// <copyright file="RecommendationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Service.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Management.DomainModels;
    using Management.Ports;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    namespace Service.Controllers
    {
        /// <summary>
        /// Controller for the safe-travel recommendations and safe-travel information for this
        /// safe-travel service.
        /// </summary>
        [Route("api/recommendations")]
        [ApiController]
        public class RecommendationController : ControllerBase
        {
            /// <summary>
            /// The port of the recommendation.
            /// </summary>
            private readonly RecommendationPort _recommendationPort;

            /// <summary>
            /// Initializes a new instance of the <see cref="RecommendationController"/> class.
            /// </summary>
            /// <param name="recommendationPort">port for the recommendation endpoints.</param>
            public RecommendationController(RecommendationPort recommendationPort)
            {
                _recommendationPort = recommendationPort ?? throw new ArgumentNullException(nameof(recommendationPort));
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
                    return Ok();
                }
                catch (Exception)
                {
                    return NotFound();
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
                    return Ok();
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }

            /// <summary>
            /// Intended to get the basic travel information about a specific state in a country,
            /// which can include COVID-19, weather, and air quality information for the user.
            /// </summary>
            /// <param name="countryCode">country code eg. "US".</param>
            /// <param name="stateCode">state code eg. "NY".</param>
            /// <returns>The state's information.</returns>
            [HttpGet]
            [Route("country/{countryCode}/state/{stateCode}")]
            public async Task<IActionResult> GetRecommendationByCountryCodeAndStateCode(
                [FromRoute] string countryCode,
                [FromRoute] string stateCode)
            {
                try
                {
                    Recommendation stateInfo = await _recommendationPort.GetLocationInfoAsync(
                                                                             new Location(Country.Wrap(countryCode), State.Wrap(stateCode)),
                                                                             UserId.Wrap("testUser"));
                    return Ok(stateInfo);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
        }
    }
}
