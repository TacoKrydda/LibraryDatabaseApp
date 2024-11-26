using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLibraryController : ControllerBase
    {
        private readonly IBookLibraryService _bookLibraryService;

        public BookLibraryController(IBookLibraryService bookLibraryService)
        {
            _bookLibraryService = bookLibraryService;
        }

        // POST: api/BookLibrary
        [HttpPost]
        public async Task<IActionResult> AddBookLibraryRelation(int bookId, int libraryId)
        {
            try
            {
                await _bookLibraryService.AddBookLibraryRelationAsync(bookId, libraryId);
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

        // DELETE: api/BookLibrary
        [HttpDelete]
        public async Task<IActionResult> RemoveBookLibraryRelation(int bookId, int libraryId)
        {
            try
            {
                await _bookLibraryService.RemoveBookLibraryRelationAsync(bookId, libraryId);
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

        // GET: api/BookLibrary/byBook/{bookId}
        [HttpGet("byBook/{bookId}")]
        public async Task<IActionResult> GetLibrariesByBook(int bookId)
        {
            try
            {
                var libraries = await _bookLibraryService.GetLibrariesByBookAsync(bookId);
                return Ok(libraries);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving libraries by book.", Details = ex.Message });
            }
        }

        // GET: api/BookLibrary/byLibrary/{libraryId}
        [HttpGet("byLibrary/{libraryId}")]
        public async Task<IActionResult> GetBooksByLibrary(int libraryId)
        {
            try
            {
                var books = await _bookLibraryService.GetBooksByLibraryAsync(libraryId);
                return Ok(books);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving books by library.", Details = ex.Message });
            }
        }
    }
}
