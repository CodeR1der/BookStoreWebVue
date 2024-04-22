using BookStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TestOperations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private readonly OrderLineDataAccess _orderLineDataAccess;

        public OrderLinesController(OrderLineDataAccess orderLineDataAccess)
        {
            _orderLineDataAccess = orderLineDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allOrderLines = _orderLineDataAccess.GetOrderLines();
            return Ok(allOrderLines);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var orderLine = _orderLineDataAccess.GetOrderLineById(id);

            if (orderLine == null)
            {
                return NotFound();
            }

            return Ok(orderLine);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] OrderLine orderLine)
        {
            if (orderLine == null)
            {
                return BadRequest();
            }

            _orderLineDataAccess.AddOrderLine(orderLine);
            return CreatedAtAction(nameof(Get), new { id = orderLine.lineID }, orderLine);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] OrderLine orderLine)
        {
            if (orderLine == null || orderLine.lineID != id)
            {
                return BadRequest();
            }

            var existingOrderLine = _orderLineDataAccess.GetOrderLineById(id);
            if (existingOrderLine == null)
            {
                return NotFound();
            }

            _orderLineDataAccess.UpdateOrderLine(orderLine);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var orderLine = _orderLineDataAccess.GetOrderLineById(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            _orderLineDataAccess.DeleteOrderLine(id);
            return NoContent();
        }
    }
}
