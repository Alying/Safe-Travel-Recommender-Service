using System;
using System.Threading.Tasks;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    /// <summary>
    /// Controller for the safe-travel recommendations and safe-travel information for this
    /// safe-travel service.
    /// </summary>
    [Route("api/recommendation")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly RecommendationPort _recommendationPort;

        public RecommendationController(RecommendationPort recommendationPort)
        {
            _recommendationPort = recommendationPort ?? throw new ArgumentNullException(nameof(recommendationPort));
        }

        /// <summary>
        /// Intended to get the top-10 recommendations for safe travel for the user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTopRecommendations()
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
        [HttpGet]
        [Route("country/{countryCode}")]
        public async Task<IActionResult> GetRecommendationByCountryCode([FromRoute] string countryCode)
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
        [HttpGet]
        [Route("country/{countryCode}/state/{state}")]
        public async Task<IActionResult> GetRecommendationByCountryCodeAndState(
            [FromRoute] string countryCode,
            [FromRoute] string state)
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
    }
}
