// <copyright file="CommentController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using ApiComment = Management.ApiModels.Comment;
using ApiUserId = Management.ApiModels.UserId;
using DomainUserId = Management.DomainModels.UserId;

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
        /// <param name="apiUserId">unique user id for the user that made the commend.</param>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="state">state code eg. "CA".</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with the response status code.</returns>
        // TODO: @mli: Get apiUserId from auth token instead of from body latter.
        [HttpGet]
        [Route("country/{countryCode}/state/{stateCode}")]
        public async Task<IActionResult> GetCommentByLocation([FromBody] ApiUserId apiUserId, [FromRoute] string countryCode, [FromRoute] string state)
        {
            Console.WriteLine($"GetCommentByLocation: userId: {apiUserId.UserIdStr}. countryCode: {countryCode}, state: {state}");
            try
            {
                return Ok(await _commentPort.GetCommentAsync(apiUserId.UserIdStr, countryCode, state));
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
        public async Task<IActionResult> CreateNewComment([FromBody] ApiComment apiComment, [FromRoute] string countryCode, [FromRoute] string stateCode)
        {
            Console.WriteLine($"CreateNewComment: countryCode: {countryCode}, stateCode: {stateCode}, body: {JsonSerializer.Serialize(apiComment)}");
            try
            {
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
