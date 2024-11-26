using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/Loan
        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            try
            {
                var loans = await _loanService.GetAllLoansAsync();
                return Ok(loans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving loans.", Details = ex.Message });
            }
        }

        // GET: api/Loan/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            try
            {
                var loan = await _loanService.GetLoanByIdAsync(id);
                return Ok(loan);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the loan.", Details = ex.Message });
            }
        }

        // POST: api/Loan
        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] LoanDTO loanDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid loan data provided." });
            }

            try
            {
                var createdLoan = await _loanService.CreateLoanAsync(loanDTO);
                return CreatedAtAction(nameof(GetLoanById), new { id = createdLoan.LoanId }, createdLoan);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the loan.", Details = ex.Message });
            }
        }

        // PUT: api/Loan/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, [FromBody] LoanDTO loanDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid loan data provided." });
            }

            try
            {
                var updatedLoan = await _loanService.UpdateLoanAsync(id, loanDTO);
                return Ok(updatedLoan);
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
                return StatusCode(500, new { Message = "An error occurred while updating the loan.", Details = ex.Message });
            }
        }

        // DELETE: api/Loan/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            try
            {
                await _loanService.DeleteLoanAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the loan.", Details = ex.Message });
            }
        }

        // GET: api/Loan/byUser/{userId}
        [HttpGet("byUser/{userId}")]
        public async Task<IActionResult> GetLoansByUser(int userId)
        {
            try
            {
                var loans = await _loanService.GetLoansByUserAsync(userId);
                return Ok(loans);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving loans by user.", Details = ex.Message });
            }
        }

        // GET: api/Loan/byBook/{bookId}
        [HttpGet("byBook/{bookId}")]
        public async Task<IActionResult> GetLoansByBook(int bookId)
        {
            try
            {
                var loans = await _loanService.GetLoansByBookAsync(bookId);
                return Ok(loans);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving loans by book.", Details = ex.Message });
            }
        }
    }
}
