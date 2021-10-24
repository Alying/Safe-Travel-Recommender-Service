using Management;
using Management.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/AseAgency/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserPort _userPort;

        public UsersController(UserPort userPort)
        {
            _userPort = userPort ?? throw new ArgumentNullException(nameof(_userPort));
        }

        [HttpGet]
        [Route("hello")]
        public async Task<IActionResult> GetHelloAsync()
        {
            return Ok("Greeting from ASE# Team!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userPort.GetAllUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{userid}")]
        public async Task<IActionResult> GetUser([FromRoute] string userid)
        {
            try
            {
                var user = await _userPort.GetUser(userid);
                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                await _userPort.InsertManyUsers(new User[] { user, });
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{userid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userid, [FromBody] User user)
        {
            try
            {
                _ = await _userPort.GetUser(userid);
                
                await _userPort.UpdateUserAsync(user);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{userid}")]
        public async Task<IActionResult> RemoveUser([FromRoute] string userid)
        {
            try
            {
                await _userPort.DeleteUserAsync(userid);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
