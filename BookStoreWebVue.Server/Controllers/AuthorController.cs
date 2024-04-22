using BookStore;
using Microsoft.AspNetCore.Mvc;

namespace TestOperations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorDataAccess _authorDataAccess;

        public AuthorsController(AuthorDataAccess authorDataAccess)
        {
            _authorDataAccess = authorDataAccess;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allAuthors = _authorDataAccess.GetAuthors();
            return Ok(allAuthors);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var address = _authorDataAccess.GetAuthorById(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            _authorDataAccess.AddAuthor(author);
            return CreatedAtAction(nameof(Get), new { id = author.authorId }, author);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] Author author)
        {
            if (author == null || author.authorId != id)
            {
                return BadRequest();
            }

            var existingBook = _authorDataAccess.GetAuthorById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            _authorDataAccess.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var book = _authorDataAccess.GetAuthorById(id);
            if (book == null)
            {
                return NotFound();
            }

            _authorDataAccess.DeleteAuthor(id);
            return NoContent();
        }
    }
}
