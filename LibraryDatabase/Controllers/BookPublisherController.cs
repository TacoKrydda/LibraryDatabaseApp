using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPublisherController : ControllerBase
    {
        private readonly IBookPublisherService _bookPublisherService;

        public BookPublisherController(IBookPublisherService bookPublisherService)
        {
            _bookPublisherService = bookPublisherService;
        }

        // POST: api/BookPublisher
        [HttpPost]
        public async Task<IActionResult> AddBookPublisherRelation(int bookId, int publisherId)
        {
            try
            {
                await _bookPublisherService.AddBookPublisherRelationAsync(bookId, publisherId);
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

        // DELETE: api/BookPublisher
        [HttpDelete]
        public async Task<IActionResult> RemoveBookPublisherRelation(int bookId, int publisherId)
        {
            try
            {
                await _bookPublisherService.RemoveBookPublisherRelationAsync(bookId, publisherId);
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

        // GET: api/BookPublisher/byBook/{bookId}
        [HttpGet("byBook/{bookId}")]
        public async Task<IActionResult> GetPublishersByBook(int bookId)
        {
            try
            {
                var publishers = await _bookPublisherService.GetPublishersByBookAsync(bookId);
                return Ok(publishers);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving publishers by book.", Details = ex.Message });
            }
        }

        // GET: api/BookPublisher/byPublisher/{publisherId}
        [HttpGet("byPublisher/{publisherId}")]
        public async Task<IActionResult> GetBooksByPublisher(int publisherId)
        {
            try
            {
                var books = await _bookPublisherService.GetBooksByPublisherAsync(publisherId);
                return Ok(books);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving books by publisher.", Details = ex.Message });
            }
        }
    }
}
