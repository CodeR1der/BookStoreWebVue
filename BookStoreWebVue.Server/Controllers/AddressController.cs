using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressesController : Controller
    {
        private readonly AddressDataAccess _addressDataAccess;

        public AddressesController(AddressDataAccess addressDataAccess)
        {
            _addressDataAccess = addressDataAccess;
        }

        // GET: api/address
        [HttpGet]
        public IActionResult Get()
        {
            var allAddresses = _addressDataAccess.GetAddresses();
            return Ok(allAddresses);
        }

        // GET: api/address/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var address = _addressDataAccess.GetAddressById(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // POST: api/books
        [HttpPost("post")]
        public IActionResult Post([FromBody] Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            _addressDataAccess.AddAddress(address);
            return CreatedAtAction(nameof(Get), new { id = address.addressId }, address);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] Address address)
        {
            if (address == null || address.addressId != id)
            {
                return BadRequest();
            }

            var existingBook = _addressDataAccess.GetAddressById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            _addressDataAccess.UpdateAddress(address);
            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var book = _addressDataAccess.GetAddressById(id);
            if (book == null)
            {
                return NotFound();
            }

            _addressDataAccess.DeleteAddress(id);
            return NoContent();
        }
    }
}
