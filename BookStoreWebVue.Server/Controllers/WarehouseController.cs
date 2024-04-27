using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreWebVue.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseDataAccess _warehouseDataAccess;

    public WarehouseController(WarehouseDataAccess warehouseDataAccess)
    {
        _warehouseDataAccess = warehouseDataAccess;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var allWarehouses = _warehouseDataAccess.GetWarehouses();
        return Ok(allWarehouses);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var warehouse = _warehouseDataAccess.GetWarehouseById(id);

        if (warehouse == null)
        {
            return NotFound();
        }

        return Ok(warehouse);
    }

    [HttpPost("post")]
    public IActionResult Post([FromBody] Warehouse warehouse)
    {
        if (warehouse == null)
        {
            return BadRequest();
        }

        _warehouseDataAccess.AddWarehouse(warehouse);
        return CreatedAtAction(nameof(Get), new { id = warehouse.warehouseId }, warehouse);
    }

    [HttpPut("{id}/update")]
    public IActionResult Put(Guid id, [FromBody] Warehouse warehouse)
    {
        if (warehouse == null || warehouse.warehouseId != id)
        {
            return BadRequest();
        }

        var existingWarehouse = _warehouseDataAccess.GetWarehouseById(id);
        if (existingWarehouse == null)
        {
            return NotFound();
        }

        _warehouseDataAccess.UpdateWarehouse(warehouse);
        return NoContent();
    }

    [HttpDelete("{id}/delete")]
    public IActionResult Delete(Guid id)
    {
        var warehouse = _warehouseDataAccess.GetWarehouseById(id);
        if (warehouse == null)
        {
            return NotFound();
        }

        _warehouseDataAccess.DeleteWarehouse(id);
        return NoContent();
    }
}
