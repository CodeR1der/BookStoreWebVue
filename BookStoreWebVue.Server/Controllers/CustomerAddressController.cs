using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerAddressController : Controller
    {
        private readonly CustomerAddressDataAccess _customerAddressDataAccess;

        public CustomerAddressController(CustomerAddressDataAccess customerAddressDataAccess)
        {
            _customerAddressDataAccess = customerAddressDataAccess;
        }
        // GET: api/author
        [HttpGet]
        public IActionResult Get()
        {
            var allcustomerAddresses = _customerAddressDataAccess.GetCustomerAddresses();
            return Ok(allcustomerAddresses);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id, Guid addId)
        {
            var customerAddress = _customerAddressDataAccess.GetCustomerAddressById(id,addId);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return Ok(customerAddress);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] CustomerAddress customerAddress)
        {
            if (customerAddress == null)
            {
                return BadRequest();
            }

            _customerAddressDataAccess.AddCustomerAddress(customerAddress);
            return CreatedAtAction(nameof(Get), new { id = customerAddress.customerId }, customerAddress);
        }

        // PUT: api/author/5
        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] CustomerAddress customerAddress, Guid adId)
        {
            if (customerAddress == null || customerAddress.customerId != id)
            {
                return BadRequest();
            }

            var existingCustomer = _customerAddressDataAccess.GetCustomerAddressById(id, adId);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customerAddressDataAccess.UpdateCustomerAddress(customerAddress);
            return NoContent();
        }

        // DELETE: api/author/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id, Guid adId)
        {
            var customer = _customerAddressDataAccess.GetCustomerAddressById(id, adId);
            if (customer == null)
            {
                return NotFound();
            }

            _customerAddressDataAccess.DeleteCustomerAddress(id, adId);
            return NoContent();
        }
    }
}
