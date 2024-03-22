using CoptisTest.Models;
using CoptisTest.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CoptisTest.API.Controllers
{
    [ApiController]
    [Route("User/[action]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await _userRepository.GetUsers());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            try
            {
                var result = await _userRepository.GetUser(userId);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                if (user == null) return BadRequest();

                await _userRepository.AddUser(user);

                return CreatedAtAction(nameof(GetUser), new { UserId = user.UserId }, null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPut("{userId:int}")]
        public async Task<ActionResult<User>> UpdateUser(int userId, User user)
        {
            try
            {
                if (userId != user.UserId)
                    return BadRequest("User ID mismatch");

                var userToUpdate = await _userRepository.GetUser(userId);

                if (userToUpdate == null)
                {
                    return NotFound($"User with Id = {userId} not found");
                }
                await _userRepository.UpdateUser(user);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<User>> DeleteUser(int userId)
        {
            try
            {
                var userToDelete = await _userRepository.GetUser(userId);

                if (userToDelete == null)
                {
                    return NotFound($"User with Id = {userId} not found");
                }
                await _userRepository.DeleteUser(userId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
