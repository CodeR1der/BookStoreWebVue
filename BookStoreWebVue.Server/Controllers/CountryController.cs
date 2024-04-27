using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly CountryDataAccess _countryDataAccess;

        public CountryController(CountryDataAccess countryDataAccess)
        {
            _countryDataAccess = countryDataAccess;
        }
        // GET: api/author
        [HttpGet]
        public IActionResult Get()
        {
            var allCountry = _countryDataAccess.GetCountryList();
            return Ok(allCountry);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var country = _countryDataAccess.GetCountryById(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }

            _countryDataAccess.AddCountry(country);
            return CreatedAtAction(nameof(Get), new { id = country.countryId }, country);
        }

        // PUT: api/author/5
        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] Country country)
        {
            if (country == null || country.countryId != id)
            {
                return BadRequest();
            }

            var existingBookAvailability = _countryDataAccess.GetCountryById(id);
            if (existingBookAvailability == null)
            {
                return NotFound();
            }

            _countryDataAccess.UpdateCountry(country);
            return NoContent();
        }

        // DELETE: api/author/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var country = _countryDataAccess.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }

            _countryDataAccess.DeleteCountry(id);
            return NoContent();
        }
    }
}
