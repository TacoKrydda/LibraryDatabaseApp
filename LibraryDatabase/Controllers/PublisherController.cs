using LibraryDatabaseClassLibrary.DTOs;
using LibraryDatabaseClassLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // GET: api/Publisher
        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            try
            {
                var publishers = await _publisherService.GetAllPublishersAsync();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving publishers.", Details = ex.Message });
            }
        }

        // GET: api/Publisher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            try
            {
                var publisher = await _publisherService.GetPublisherByIdAsync(id);
                return Ok(publisher);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the publisher.", Details = ex.Message });
            }
        }

        // POST: api/Publisher
        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromBody] PublisherDTO publisherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid publisher data provided." });
            }

            try
            {
                var createdPublisher = await _publisherService.CreatePublisherAsync(publisherDTO);
                return CreatedAtAction(nameof(GetPublisherById), new { id = createdPublisher.PublisherId }, createdPublisher);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the publisher.", Details = ex.Message });
            }
        }

        // PUT: api/Publisher/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherDTO publisherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid publisher data provided." });
            }

            try
            {
                var updatedPublisher = await _publisherService.UpdatePublisherAsync(id, publisherDTO);
                return Ok(updatedPublisher);
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
                return StatusCode(500, new { Message = "An error occurred while updating the publisher.", Details = ex.Message });
            }
        }

        // DELETE: api/Publisher/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                await _publisherService.DeletePublisherAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the publisher.", Details = ex.Message });
            }
        }
    }
}
