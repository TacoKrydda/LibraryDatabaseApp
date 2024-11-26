using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookGenreController : ControllerBase
    {
        private readonly IBookGenreService _bookGenreService;

        public BookGenreController(IBookGenreService bookGenreService)
        {
            _bookGenreService = bookGenreService;
        }

        // POST: api/BookGenre
        [HttpPost]
        public async Task<IActionResult> AddBookGenreRelation(int bookId, int genreId)
        {
            try
            {
                await _bookGenreService.AddBookGenreRelationAsync(bookId, genreId);
                return Ok(new { Message = "Relation added successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the relation.", Details = ex.Message });
            }
        }

        // DELETE: api/BookGenre
        [HttpDelete]
        public async Task<IActionResult> RemoveBookGenreRelation(int bookId, int genreId)
        {
            try
            {
                await _bookGenreService.RemoveBookGenreRelationAsync(bookId, genreId);
                return Ok(new { Message = "Relation removed successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while removing the relation.", Details = ex.Message });
            }
        }

        // GET: api/BookGenre/byBook/{bookId}
        [HttpGet("byBook/{bookId}")]
        public async Task<IActionResult> GetGenresByBook(int bookId)
        {
            try
            {
                var genres = await _bookGenreService.GetGenresByBookAsync(bookId);
                return Ok(genres);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving genres by book.", Details = ex.Message });
            }
        }

        // GET: api/BookGenre/byGenre/{genreId}
        [HttpGet("byGenre/{genreId}")]
        public async Task<IActionResult> GetBooksByGenre(int genreId)
        {
            try
            {
                var books = await _bookGenreService.GetBooksByGenreAsync(genreId);
                return Ok(books);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving books by genre.", Details = ex.Message });
            }
        }
    }
}
