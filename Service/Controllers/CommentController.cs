using Management;
using Management.ApiModels;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Management.DomainModels;
using System.Text.Json;

using ApiComment = Management.ApiModels.Comment;

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
        [Route("country/{countryCode}/state/{stateCode}")]
        public async Task<IActionResult> GetCommentByLocation([FromRoute] string countryCode, [FromRoute] string stateCode)
        {
            Console.WriteLine("GetCommentByLocation: countryCode: " + countryCode + ", stateCode: " + stateCode);
            try
            {
                var getCommentTask = _commentPort.GetCommentAsync(UserId.Wrap("testUser"), new Location(Country.Wrap(countryCode), State.Wrap(stateCode)));
                getCommentTask.Wait();
                var rtn = JsonSerializer.Serialize(getCommentTask.Result);
                Console.WriteLine(rtn);
                return Ok(getCommentTask.Result);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetComment caught exception: " + e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [Route("country/{countryCode}/state/{stateCode}")]
        public async Task<IActionResult> CreateNewComment([FromBody] string commentStr, [FromRoute] string countryCode, [FromRoute] string stateCode)
        {
            Console.WriteLine("CreateNewComment: countryCode: " + countryCode + ", stateCode: " + stateCode + ", comment: " + commentStr);
            try
            {
                _commentPort.AddCommentAsync(UserId.Wrap("testUser"), new Location(Country.Wrap(countryCode), State.Wrap(stateCode)), commentStr).Wait();
                return Ok();
            } catch(Exception e)
            {
                Console.WriteLine("CreateNewComment caught exception: " + e.Message);
                return NotFound();
            }
        }
    }
}
