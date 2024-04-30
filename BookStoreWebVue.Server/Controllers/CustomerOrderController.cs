using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerOrdersController : Controller
    {
        private readonly CustomerOrderDataAccess _customerOrderDataAccess;

        public CustomerOrdersController(CustomerOrderDataAccess customerOrderDataAccess)
        {
            _customerOrderDataAccess = customerOrderDataAccess;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allcustomerAddresses = _customerOrderDataAccess.GetCustomerOrders();
            return Ok(allcustomerAddresses);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id, Guid addId)
        {
            var customerAddress = _customerOrderDataAccess.GetCustomerOrderById(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return Ok(customerAddress);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] CustomerOrder customerOrder)
        {
            if (customerOrder == null)
            {
                return BadRequest();
            }
            customerOrder.orderId = Guid.NewGuid();
            _customerOrderDataAccess.AddCustomerOrder(customerOrder);
            return CreatedAtAction(nameof(Get), new { id = customerOrder.customerId }, customerOrder);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] CustomerOrder customerOrder)
        {
            if (customerOrder == null || customerOrder.customerId != id)
            {
                return BadRequest();
            }

            var existingCustomer = _customerOrderDataAccess.GetCustomerOrderById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customerOrderDataAccess.UpdateCustomerOrder(customerOrder);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var customer = _customerOrderDataAccess.GetCustomerOrderById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerOrderDataAccess.DeleteCustomerOrder(id);
            return NoContent();
        }
    }
}
