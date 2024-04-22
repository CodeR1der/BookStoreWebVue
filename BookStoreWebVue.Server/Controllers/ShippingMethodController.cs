using BookStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TestOperations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShippingMethodController : ControllerBase
    {
        private readonly ShippingMethodDataAccess _shippingMethodDataAccess;

        public ShippingMethodController(ShippingMethodDataAccess shippingMethodDataAccess)
        {
            _shippingMethodDataAccess = shippingMethodDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allShippingMethods = _shippingMethodDataAccess.GetShippingMethods();
            return Ok(allShippingMethods);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var shippingMethod = _shippingMethodDataAccess.GetShippingMethodById(id);

            if (shippingMethod == null)
            {
                return NotFound();
            }

            return Ok(shippingMethod);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] ShippingMethod shippingMethod)
        {
            if (shippingMethod == null)
            {
                return BadRequest();
            }

            _shippingMethodDataAccess.AddShippingMethod(shippingMethod);
            return CreatedAtAction(nameof(Get), new { id = shippingMethod.methodId }, shippingMethod);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] ShippingMethod shippingMethod)
        {
            if (shippingMethod == null || shippingMethod.methodId != id)
            {
                return BadRequest();
            }

            var existingShippingMethod = _shippingMethodDataAccess.GetShippingMethodById(id);
            if (existingShippingMethod == null)
            {
                return NotFound();
            }

            _shippingMethodDataAccess.UpdateShippingMethod(shippingMethod);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var publisher = _shippingMethodDataAccess.GetShippingMethodById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _shippingMethodDataAccess.DeleteShippingMethod(id);
            return NoContent();
        }
    }
}
