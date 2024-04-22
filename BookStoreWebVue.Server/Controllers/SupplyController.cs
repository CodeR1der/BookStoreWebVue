using BookStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TestOperations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private readonly SupplyDataAccess _supplyDataAccess;

        public SupplyController(SupplyDataAccess supplyDataAccess)
        {
            _supplyDataAccess = supplyDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allSupplies = _supplyDataAccess.GetSupplies();
            return Ok(allSupplies);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supply = _supplyDataAccess.GetSupplyById(id);

            if (supply == null)
            {
                return NotFound();
            }

            return Ok(supply);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Supply supply)
        {
            if (supply == null)
            {
                return BadRequest();
            }

            _supplyDataAccess.AddSupply(supply);
            return CreatedAtAction(nameof(Get), new { id = supply.supplyId }, supply);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] Supply supply)
        {
            if (supply == null || supply.supplyId != id)
            {
                return BadRequest();
            }

            var existingSupply = _supplyDataAccess.GetSupplyById(id);
            if (existingSupply == null)
            {
                return NotFound();
            }

            _supplyDataAccess.UpdateSupply(supply);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var supply = _supplyDataAccess.GetSupplyById(id);
            if (supply == null)
            {
                return NotFound();
            }

            _supplyDataAccess.DeleteSupply(id);
            return NoContent();
        }
    }
}
