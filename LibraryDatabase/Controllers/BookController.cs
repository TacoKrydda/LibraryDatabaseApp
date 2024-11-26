using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving books.", Details = ex.Message });
            }
        }

        // GET: api/Book/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the book.", Details = ex.Message });
            }
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid book data provided." });
            }

            try
            {
                var createdBook = await _bookService.CreateBookAsync(bookDTO);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.BookId }, createdBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the book.", Details = ex.Message });
            }
        }

        // PUT: api/Book/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid book data provided." });
            }

            try
            {
                var updatedBook = await _bookService.UpdateBookAsync(id, bookDTO);
                return Ok(updatedBook);
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
                return StatusCode(500, new { Message = "An error occurred while updating the book.", Details = ex.Message });
            }
        }

        // DELETE: api/Book/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the book.", Details = ex.Message });
            }
        }
    }
}
