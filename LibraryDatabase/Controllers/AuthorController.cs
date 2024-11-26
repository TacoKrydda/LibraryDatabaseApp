using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving authors.", Details = ex.Message });
            }
        }

        // GET: api/Author/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the author.", Details = ex.Message });
            }
        }

        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid author data provided." });
            }

            try
            {
                var createdAuthor = await _authorService.CreateAuthorAsync(authorDTO);
                return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.AuthorId }, createdAuthor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the author.", Details = ex.Message });
            }
        }

        // PUT: api/Author/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid author data provided." });
            }

            try
            {
                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorDTO);
                return Ok(updatedAuthor);
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
                return StatusCode(500, new { Message = "An error occurred while updating the author.", Details = ex.Message });
            }
        }

        // DELETE: api/Author/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the author.", Details = ex.Message });
            }
        }
    }
}
