using CoptisTest.API.Repositories;
using CoptisTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoptisTest.API.Controllers
{
    [ApiController]
    [Route("Access/[action]")]
    public class AccessController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        private readonly IAccessRepository _accessRepository;
        private readonly IUserRepository _userRepository;

        public AccessController(ILogger<AccessController> logger, IAccessRepository accessRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _accessRepository = accessRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAccesses()
        {
            try
            {
                return Ok(await _accessRepository.GetAccesses());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{accessId:int}")]
        public async Task<ActionResult<Access>> GetAccess(int accessId)
        {
            try
            {
                var result = await _accessRepository.GetAccess(accessId);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{userId:int}")]
        public async Task<ActionResult> GetUserAccesses(int userId)
        {
            try
            {
                var userToGet = await _userRepository.GetUser(userId);

                if (userToGet == null)
                {
                    return NotFound($"User with Id = {userId} not found");
                }
                return Ok(await _accessRepository.GetUserAccesses(userId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddAccessToUser(int userId, int accessId)
        {
            try
            {
                var userToAdd = await _userRepository.GetUser(userId);

                if (userToAdd == null)
                {
                    return NotFound($"User with Id = {userId} not found");
                }
                var accessToAdd = await _accessRepository.GetAccess(accessId);

                if (accessToAdd == null)
                {
                    return NotFound($"Access with Id = {accessId} not found");
                }
                await _accessRepository.AddAccessToUser(userId, accessId);
                return CreatedAtAction(nameof(GetAccess), new { accessId = accessId }, null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpDelete("{userId:int},{accessId:int}")]
        public async Task<ActionResult> DeleteAccessToUser(int userId, int accessId)
        {
            try
            {
                var userToDelete = await _userRepository.GetUser(userId);

                if (userToDelete == null)
                {
                    return NotFound($"User with Id = {userId} not found");
                }
                var accessToDelete = await _accessRepository.GetAccess(accessId);

                if (accessToDelete == null)
                {
                    return NotFound($"Access with Id = {accessId} not found");
                }
                var userAccessToDelete = await _accessRepository.GetUserAccesses(userId);

                if (!userAccessToDelete.Any(x => x.AccessId == accessId))
                {
                    return NotFound($"UserAccess with UserId = {userId} and AccessId = {accessId} not found");
                }
                await _accessRepository.DeleteAccessToUser(userId, accessId);
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
