using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;

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
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The state's information.</returns>
        [HttpGet]
        [Route("country/{countryCode}/state/{stateCode}")]
        public async Task<IActionResult> GetRecommendationByCountryCodeAndStateCode(
            [FromRoute] string countryCode,
            [FromRoute] string stateCode,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _recommendationPort.GetRecommendationsCitiesWithScoreAsync(countryCode, stateCode, cancellationToken));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
