using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        // GET: api/Library
        [HttpGet]
        public async Task<IActionResult> GetAllLibraries()
        {
            try
            {
                var libraries = await _libraryService.GetAllLibrariesAsync();
                return Ok(libraries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving libraries.", Details = ex.Message });
            }
        }

        // GET: api/Library/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibraryById(int id)
        {
            try
            {
                var library = await _libraryService.GetLibraryByIdAsync(id);
                return Ok(library);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the library.", Details = ex.Message });
            }
        }

        // POST: api/Library
        [HttpPost]
        public async Task<IActionResult> CreateLibrary([FromBody] LibraryDTO libraryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid library data provided." });
            }

            try
            {
                var createdLibrary = await _libraryService.CreateLibraryAsync(libraryDTO);
                return CreatedAtAction(nameof(GetLibraryById), new { id = createdLibrary.LibraryId }, createdLibrary);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the library.", Details = ex.Message });
            }
        }

        // PUT: api/Library/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibrary(int id, [FromBody] LibraryDTO libraryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid library data provided." });
            }

            try
            {
                var updatedLibrary = await _libraryService.UpdateLibraryAsync(id, libraryDTO);
                return Ok(updatedLibrary);
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
                return StatusCode(500, new { Message = "An error occurred while updating the library.", Details = ex.Message });
            }
        }

        // DELETE: api/Library/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibrary(int id)
        {
            try
            {
                await _libraryService.DeleteLibraryDeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the library.", Details = ex.Message });
            }
        }
    }
}
