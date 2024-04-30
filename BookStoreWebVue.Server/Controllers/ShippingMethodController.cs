using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShippingMethodsController : ControllerBase
    {
        private readonly ShippingMethodDataAccess _shippingMethodDataAccess;

        public ShippingMethodsController(ShippingMethodDataAccess shippingMethodDataAccess)
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
        public IActionResult Get(Guid id)
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
        public IActionResult Put(Guid id, [FromBody] ShippingMethod shippingMethod)
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
        public IActionResult Delete(Guid id)
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
