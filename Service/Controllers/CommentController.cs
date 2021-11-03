using Management;
using Management.ApiModels;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentPort _commentPort;

        public CommentController(CommentPort commentPort)
        {
            _commentPort = commentPort ?? throw new ArgumentNullException(nameof(commentPort));
        }

        [HttpGet]
        [Route("{locationId}")]
        public async Task<IActionResult> GetCommentByLocationId([FromRoute] string locationId)
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

        [HttpPost]
        public async Task<IActionResult> CreateNewComment([FromBody] Comment comment)
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

        [HttpPut]
        [Route("{locationId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string locationId, [FromBody] CommentUpdateRequest request)
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
