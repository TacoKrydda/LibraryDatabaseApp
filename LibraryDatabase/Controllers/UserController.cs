using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving users.", Details = ex.Message });
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the user.", Details = ex.Message });
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid user data provided." });
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(userDTO);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the user.", Details = ex.Message });
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid user data provided." });
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDTO);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the user.", Details = ex.Message });
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the user.", Details = ex.Message });
            }
        }
    }
}
