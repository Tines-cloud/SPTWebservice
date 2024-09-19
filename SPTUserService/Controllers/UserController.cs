using Microsoft.AspNetCore.Mvc;
using SPTUserService.DTO;
using SPTUserService.Services;

namespace SPTUserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDTO>> GetUser(string username)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);

                if (user == null)
                {
                    return NotFound($"User with username {username} not found.");
                }

                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            if (await _userService.UserExistsAsync(user.Username))
            {
                return Conflict($"User with username {user.Username} already exists.");
            }

            try
            {
                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, UserDTO user)
        {
            if (username != user.Username)
            {
                return BadRequest("Username mismatch.");
            }

            if (!await _userService.UserExistsAsync(username))
            {
                return NotFound($"User with username {username} not found.");
            }

            try
            {
                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            if (!await _userService.UserExistsAsync(username))
            {
                return NotFound($"User with username {username} not found.");
            }

            try
            {
                await _userService.DeleteUserAsync(username);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Exists/{username}")]
        public async Task<IActionResult> UserExists(string username)
        {
            try
            {
                var exists = await _userService.UserExistsAsync(username);
                return Ok(exists);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest("Login data is null.");
            }

            try
            {
                var isValidUser = await _userService.LoginAsync(login.Username, login.Password);
                if (!isValidUser)
                {
                    return Unauthorized("Invalid username or password.");
                }

                return Ok(new { message = "Login successful." });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
