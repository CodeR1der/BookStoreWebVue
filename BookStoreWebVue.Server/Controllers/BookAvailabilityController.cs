using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookAvailabilityController : Controller
    {
        private readonly BookAvailabilityDataAccess _bookAvailabilityDataAccess;

        public BookAvailabilityController(BookAvailabilityDataAccess bookAvailabilityDataAccess)
        {
            _bookAvailabilityDataAccess = bookAvailabilityDataAccess;
        }
        // GET: api/author
        [HttpGet]
        public IActionResult Get()
        {
            var allBookAvailabilities = _bookAvailabilityDataAccess.GetBookAvailabilities();
            return Ok(allBookAvailabilities);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var bookAvailability = _bookAvailabilityDataAccess.GetBookAvailabilityById(id);

            if (bookAvailability == null)
            {
                return NotFound();
            }

            return Ok(bookAvailability);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] BookAvailability bookAvailability)
        {
            if (bookAvailability == null)
            {
                return BadRequest();
            }

            _bookAvailabilityDataAccess.AddBookAvailability(bookAvailability);
            return CreatedAtAction(nameof(Get), new { id = bookAvailability.availabilityId }, bookAvailability);
        }

        // PUT: api/author/5
        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] BookAvailability bookAvailability)
        {
            if (bookAvailability == null || bookAvailability.availabilityId != id)
            {
                return BadRequest();
            }

            var existingBookAvailability = _bookAvailabilityDataAccess.GetBookAvailabilityById(id);
            if (existingBookAvailability == null)
            {
                return NotFound();
            }

            _bookAvailabilityDataAccess.UpdateBookAvailability(bookAvailability);
            return NoContent();
        }

        // DELETE: api/author/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var bookAvailability = _bookAvailabilityDataAccess.GetBookAvailabilityById(id);
            if (bookAvailability == null)
            {
                return NotFound();
            }

            _bookAvailabilityDataAccess.DeleteBookAvailability(id);
            return NoContent();
        }
    }
}
