using Microsoft.AspNetCore.Mvc;
using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using System;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDataAccess _bookDataAccess;
        private readonly IWebHostEnvironment _environment;
        public BooksController(BookDataAccess bookDataAccess, IWebHostEnvironment environment)
        {
            _bookDataAccess = bookDataAccess;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allBooks = _bookDataAccess.GetAllBooksWithCover();

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
        public IActionResult Post([FromForm] BookFormData formData)
        {
            var book = formData.Book;
            var coverImage = formData.CoverImage;
            book.bookId = Guid.NewGuid();

            if (book == null || coverImage == null)
            {
                return BadRequest();
            }

            if (coverImage.Length > 0)
            {
                var imagePath = Path.Combine(_environment.ContentRootPath, "ImageBooks", $"{book.bookId.ToString("D")}.jpg");
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    coverImage.CopyTo(fileStream);
                }
            }

            _bookDataAccess.AddBook(book);
            return CreatedAtAction(nameof(Get), book);
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
        public class BookFormData
        {
            public Book Book { get; set; }
            public IFormFile CoverImage { get; set; }
        }

    }
}
