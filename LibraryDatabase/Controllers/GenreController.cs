using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenresAsync();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving genres.", Details = ex.Message });
            }
        }

        // GET: api/Genre/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                return Ok(genre);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the genre.", Details = ex.Message });
            }
        }

        // POST: api/Genre
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid genre data provided." });
            }

            try
            {
                var createdGenre = await _genreService.CreateGenreAsync(genreDTO);
                return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.GenreId }, createdGenre);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the genre.", Details = ex.Message });
            }
        }

        // PUT: api/Genre/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDTO genreDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid genre data provided." });
            }

            try
            {
                var updatedGenre = await _genreService.UpdateGenreAsync(id, genreDTO);
                return Ok(updatedGenre);
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
                return StatusCode(500, new { Message = "An error occurred while updating the genre.", Details = ex.Message });
            }
        }

        // DELETE: api/Genre/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                await _genreService.DeleteGenreAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the genre.", Details = ex.Message });
            }
        }
    }
}
