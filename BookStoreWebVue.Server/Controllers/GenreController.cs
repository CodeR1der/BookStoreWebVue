using BookStore;
using Microsoft.AspNetCore.Mvc;
using TestOperations;

namespace BookStoreWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : Controller
    {
        private readonly GenreDataAccess _genreOrderDataAccess;

        public GenresController(GenreDataAccess genreOrderDataAccess)
        {
            _genreOrderDataAccess = genreOrderDataAccess;
        }
        // GET: api/author
        [HttpGet]
        public IActionResult Get()
        {
            var allcustomerAddresses = _genreOrderDataAccess.GetCGenres();
            return Ok(allcustomerAddresses);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customerAddress = _genreOrderDataAccess.GetGenreById(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return Ok(customerAddress);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }

            _genreOrderDataAccess.AddGenre(genre);
            return CreatedAtAction(nameof(Get), new { id = genre.genreId }, genre);
        }

        // PUT: api/author/5
        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] Genre genre)
        {
            if (genre == null || genre.genreId != id)
            {
                return BadRequest();
            }

            var existingCustomer = _genreOrderDataAccess.GetGenreById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _genreOrderDataAccess.UpdateGenre(genre);
            return NoContent();
        }

        // DELETE: api/author/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var customer = _genreOrderDataAccess.GetGenreById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _genreOrderDataAccess.DeleteGenre(id);
            return NoContent();
        }
    }
}
