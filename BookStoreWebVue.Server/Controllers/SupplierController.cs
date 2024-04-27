using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierDataAccess _supplierDataAccess;

        public SupplierController(SupplierDataAccess supplierDataAccess)
        {
            _supplierDataAccess = supplierDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allSuppliers = _supplierDataAccess.GetSuppliers();
            return Ok(allSuppliers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var supplier = _supplierDataAccess.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }

            _supplierDataAccess.AddSupplier(supplier);
            return CreatedAtAction(nameof(Get), new { id = supplier.supplierId }, supplier);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] Supplier supplier)
        {
            if (supplier == null || supplier.supplierId != id)
            {
                return BadRequest();
            }

            var existingSupplier = _supplierDataAccess.GetSupplierById(id);
            if (existingSupplier == null)
            {
                return NotFound();
            }

            _supplierDataAccess.UpdateSupplier(supplier);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var supplier = _supplierDataAccess.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _supplierDataAccess.DeleteSupplier(id);
            return NoContent();
        }
    }
}
