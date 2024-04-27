using Microsoft.AspNetCore.Mvc;
using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDataAccess _bookDataAccess;

        public BooksController(BookDataAccess bookDataAccess)
        {
            _bookDataAccess = bookDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allBooks = _bookDataAccess.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var book = _bookDataAccess.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            
            _bookDataAccess.AddBook(book);
            return CreatedAtAction(nameof(Get), new { id = book.bookId }, book);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] Book book)
        {
            if (book == null || book.bookId != id)
            {
                return BadRequest();
            }

            var existingBook = _bookDataAccess.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            _bookDataAccess.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var book = _bookDataAccess.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookDataAccess.DeleteBook(id);
            return NoContent();
        }
    }
}
