using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login login) 
        {
            var usersdto = await _userRepository.LoginAsync(login.username, login.password);
            if(usersdto != null)
            {
                return Ok("Success Login!");
            }
            else
            {
                return BadRequest("please re-check the username or password");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] UserDTO user)
        {
            await _userRepository.AddUsersAsync(user);
            return Ok("Successfully Create User!");
        }
    }
}
