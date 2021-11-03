using Management;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/recommendation")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly RecommendationPort _recommendationPort;

        public RecommendationController(RecommendationPort recommendationPort) 
        {
            _recommendationPort = recommendationPort ?? throw new ArgumentNullException(nameof(recommendationPort));
        }

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

        [HttpGet]
        [Route("{countryCode}")]
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
    }
}
