using BookStore;
using Microsoft.AspNetCore.Mvc;
using TestOperations;

namespace BookStoreWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDataAccess _customerDataAccess;

        public CustomersController(CustomerDataAccess customerDataAccess)
        {
            _customerDataAccess = customerDataAccess;
        }
        // GET: api/author
        [HttpGet]
        public IActionResult Get()
        {
            var allcustomers = _customerDataAccess.GetCustomers();
            return Ok(allcustomers);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customerDataAccess.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _customerDataAccess.AddCustomer(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.customerId }, customer);
        }

        // PUT: api/author/5
        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.customerId != id)
            {
                return BadRequest();
            }

            var existingCustomer = _customerDataAccess.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customerDataAccess.UpdateCustomer(customer);
            return NoContent();
        }

        // DELETE: api/author/5
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var customer = _customerDataAccess.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerDataAccess.DeleteCustomer(id);
            return NoContent();
        }
    }
}
