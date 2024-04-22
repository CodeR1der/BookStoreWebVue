using BookStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TestOperations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly OrderStatusDataAccess _orderStatusDataAccess;

        public OrderStatusController(OrderStatusDataAccess orderStatusDataAccess)
        {
            _orderStatusDataAccess = orderStatusDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allOrderStatuses = _orderStatusDataAccess.GetOrderStatuses();
            return Ok(allOrderStatuses);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var orderStatus = _orderStatusDataAccess.GetOrderStatusById(id);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] OrderStatus orderStatus)
        {
            if (orderStatus == null)
            {
                return BadRequest();
            }

            _orderStatusDataAccess.AddOrderStatus(orderStatus);
            return CreatedAtAction(nameof(Get), new { id = orderStatus.statusId }, orderStatus);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] OrderStatus orderStatus)
        {
            if (orderStatus == null || orderStatus.statusId != id)
            {
                return BadRequest();
            }

            var existingOrderStatus = _orderStatusDataAccess.GetOrderStatusById(id);
            if (existingOrderStatus == null)
            {
                return NotFound();
            }

            _orderStatusDataAccess.UpdateOrderStatus(orderStatus);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var orderStatus = _orderStatusDataAccess.GetOrderStatusById(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            _orderStatusDataAccess.DeleteOrderStatus(id);
            return NoContent();
        }
    }
}
