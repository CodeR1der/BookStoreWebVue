using BookStore;
using Microsoft.AspNetCore.Mvc;
using TestOperations;

namespace BookStoreWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderHistoryController : Controller
    {
        private readonly OrderHistoryDataAccess _OrderHistoryOrderDataAccess;

        public OrderHistoryController(OrderHistoryDataAccess OrderHistoryDataAccess)
        {
            _OrderHistoryOrderDataAccess = OrderHistoryDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allcustomerAddresses = _OrderHistoryOrderDataAccess.GetOrderHistories();
            return Ok(allcustomerAddresses);
        }

        // GET: api/author/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customerAddress = _OrderHistoryOrderDataAccess.GetOrderHistoryById(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return Ok(customerAddress);
        }

        // POST: api/author
        [HttpPost("post")]
        public IActionResult Post([FromBody] OrderHistory orderHistory)
        {
            if (orderHistory == null)
            {
                return BadRequest();
            }

            _OrderHistoryOrderDataAccess.AddOrderHistory(orderHistory);
            return CreatedAtAction(nameof(Get), new { id = orderHistory.orderId }, orderHistory);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] OrderHistory orderHistory)
        {
            if (orderHistory == null || orderHistory.orderId != id)
            {
                return BadRequest();
            }

            var existingCustomer = _OrderHistoryOrderDataAccess.GetOrderHistoryById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _OrderHistoryOrderDataAccess.UpdateOrderHistory(orderHistory);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var customer = _OrderHistoryOrderDataAccess.GetOrderHistoryById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _OrderHistoryOrderDataAccess.DeleteOrderHistory(id);
            return NoContent();
        }
    }
}
