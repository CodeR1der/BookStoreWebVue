using BookStore;
using Microsoft.AspNetCore.Mvc;
using TestOperations;

namespace BookStoreWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly LanguageDataAccess _languageDataAccess;

        public LanguagesController(LanguageDataAccess languageDataAccess)
        {
            _languageDataAccess = languageDataAccess;
        }
        // GET: api/author
        [HttpGet]
        public IActionResult Get()
        {
            var allLanguages = _languageDataAccess.GetBookLanguages();
            return Ok(allLanguages);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var language = _languageDataAccess.GetLanguageIdById(id);

            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] BookLanguage language)
        {
            if (language == null)
            {
                return BadRequest();
            }

            _languageDataAccess.AddLanguage(language);
            return CreatedAtAction(nameof(Get), new { id = language.languageId }, language);
        }

        // PUT: api/author/5
        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] BookLanguage language)
        {
            if (language == null || language.languageId != id)
            {
                return BadRequest();
            }

            var existingBookAvailability = _languageDataAccess.GetLanguageIdById(id);
            if (existingBookAvailability == null)
            {
                return NotFound();
            }

            _languageDataAccess.UpdateLanguage(language);
            return NoContent();
        }

        // DELETE: api/author/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var language = _languageDataAccess.GetLanguageIdById(id);
            if (language == null)
            {
                return NotFound();
            }

            _languageDataAccess.DeleteLanguage(id);
            return NoContent();
        }
    }
}
