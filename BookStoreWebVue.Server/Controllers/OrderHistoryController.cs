using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;


namespace BookStoreWebVue.Server.Controllers
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
        public IActionResult Get(Guid id)
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
        public IActionResult Put(Guid id, [FromBody] OrderHistory orderHistory)
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
        public IActionResult Delete(Guid id)
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
