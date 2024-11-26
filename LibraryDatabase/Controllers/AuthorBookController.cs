using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        private readonly IAuthorBookService _authorBookService;

        public AuthorBookController(IAuthorBookService authorBookService)
        {
            _authorBookService = authorBookService;
        }

        // POST: api/AuthorBook
        [HttpPost]
        public async Task<IActionResult> AddAuthorBookRelation(int authorId, int bookId)
        {
            try
            {
                await _authorBookService.AddAuthorBookRelationAsync(authorId, bookId);
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

        // DELETE: api/AuthorBook
        [HttpDelete]
        public async Task<IActionResult> RemoveAuthorBookRelation(int authorId, int bookId)
        {
            try
            {
                await _authorBookService.RemoveAuthorBookRelationAsync(authorId, bookId);
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

        // GET: api/AuthorBook/byAuthor/{authorId}
        [HttpGet("byAuthor/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            try
            {
                var books = await _authorBookService.GetBooksByAuthorAsync(authorId);
                return Ok(books);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving books by author.", Details = ex.Message });
            }
        }

        // GET: api/AuthorBook/byBook/{bookId}
        [HttpGet("byBook/{bookId}")]
        public async Task<IActionResult> GetAuthorsByBook(int bookId)
        {
            try
            {
                var authors = await _authorBookService.GetAuthorsByBookAsync(bookId);
                return Ok(authors);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving authors by book.", Details = ex.Message });
            }
        }
    }
}
