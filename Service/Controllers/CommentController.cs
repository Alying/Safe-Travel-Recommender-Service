using System;
using System.Text.Json;
using System.Threading.Tasks;
using Common;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using ApiComment = Management.ApiModels.Comment;
using ApiUserId = Management.ApiModels.UserId;

namespace Service.Controllers
{
    /// <summary>
    /// Controller for the user commenting system for this safe-travel service.
    /// </summary>
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentPort _commentPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="commentPort">port for comment endpoints.</param>
        public CommentController(CommentPort commentPort)
        {
            _commentPort = commentPort ?? throw new ArgumentNullException(nameof(commentPort));
        }

        /// <summary>
        /// Intended to get the comments for the specified country and state for the user.
        /// </summary>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="state">state code eg. "CA".</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with the response status code.</returns>
        [HttpGet]
        [Route("country/{countryCode}/state/{state}")]
        public async Task<IActionResult> GetCommentByLocation([FromHeader] string authorization, [FromRoute] string countryCode, [FromRoute] string state)
        {
            Console.WriteLine($"Authorization: {authorization}");
            try
            {
                var userEmail = TokenUtils.validAuthHeader(authorization);
                Console.WriteLine($"GetCommentByLocation: userId: {userEmail}. countryCode: {countryCode}, state: {state}");
                return Ok(await _commentPort.GetCommentAsync(userEmail, countryCode, state));
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetComment caught exception: {e.Message}");
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Intended to post the user's comments for this specific country and state.
        /// </summary>
        /// <param name="apiComment">The comment that the user made.</param>
        /// <param name="countryCode">The country that the comment was made on.</param>
        /// <param name="stateCode">The state that the comment was made on.</param>
        /// <returns>The state's information.</returns>
        [HttpPost]
        [Route("country/{countryCode}/state/{stateCode}")]
        public async Task<IActionResult> CreateNewComment([FromHeader] string authorization, [FromBody] ApiComment apiComment, [FromRoute] string countryCode, [FromRoute] string stateCode)
        {
            Console.WriteLine($"CreateNewComment: countryCode: {countryCode}, stateCode: {stateCode}, body: {JsonSerializer.Serialize(apiComment)}");
            try
            {
                var userEmail = TokenUtils.validAuthHeader(authorization);
                Console.WriteLine($"userEmail: {userEmail}");
                apiComment.UserIdStr = userEmail;
                await _commentPort.AddCommentAsync(countryCode, stateCode, apiComment);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"CreateNewComment caught exception: {e.Message}");
                return NotFound(e.Message);
            }
        }
    }
}
