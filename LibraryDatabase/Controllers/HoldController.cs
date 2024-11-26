using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoldController : ControllerBase
    {
        private readonly IHoldService _holdService;

        public HoldController(IHoldService holdService)
        {
            _holdService = holdService;
        }

        // GET: api/Hold
        [HttpGet]
        public async Task<IActionResult> GetAllHolds()
        {
            try
            {
                var holds = await _holdService.GetAllHoldsAsync();
                return Ok(holds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving holds.", Details = ex.Message });
            }
        }

        // GET: api/Hold/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHoldById(int id)
        {
            try
            {
                var hold = await _holdService.GetHoldByIdAsync(id);
                return Ok(hold);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the hold.", Details = ex.Message });
            }
        }

        // POST: api/Hold
        [HttpPost]
        public async Task<IActionResult> CreateHold([FromBody] HoldDTO holdDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid hold data provided." });
            }

            try
            {
                var createdHold = await _holdService.CreateHoldAsync(holdDTO);
                return CreatedAtAction(nameof(GetHoldById), new { id = createdHold.HoldId }, createdHold);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the hold.", Details = ex.Message });
            }
        }

        // PUT: api/Hold/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHold(int id, [FromBody] HoldDTO holdDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid hold data provided." });
            }

            try
            {
                var updatedHold = await _holdService.UpdateHoldAsync(id, holdDTO);
                return Ok(updatedHold);
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
                return StatusCode(500, new { Message = "An error occurred while updating the hold.", Details = ex.Message });
            }
        }

        // DELETE: api/Hold/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHold(int id)
        {
            try
            {
                await _holdService.DeleteHoldAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the hold.", Details = ex.Message });
            }
        }

        // GET: api/Hold/byUser/{userId}
        [HttpGet("byUser/{userId}")]
        public async Task<IActionResult> GetHoldsByUser(int userId)
        {
            try
            {
                var holds = await _holdService.GetHoldsByUserAsync(userId);
                return Ok(holds);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving holds by user.", Details = ex.Message });
            }
        }

        // GET: api/Hold/byBook/{bookId}
        [HttpGet("byBook/{bookId}")]
        public async Task<IActionResult> GetHoldsByBook(int bookId)
        {
            try
            {
                var holds = await _holdService.GetHoldsByBookAsync(bookId);
                return Ok(holds);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving holds by book.", Details = ex.Message });
            }
        }
    }
}
