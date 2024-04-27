using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using BookStoreWebVue.Server.Functions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreWebVue.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class SupplyLineController : ControllerBase
{
    private readonly SupplyLineDataAccess _supplyLineDataAccess;

    public SupplyLineController(SupplyLineDataAccess supplyLineDataAccess)
    {
        _supplyLineDataAccess = supplyLineDataAccess;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var allSupplyLines = _supplyLineDataAccess.GetSupplyLines();
        return Ok(allSupplyLines);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var supplyLine = _supplyLineDataAccess.GetSupplyLineById(id);

        if (supplyLine == null)
        {
            return NotFound();
        }

        return Ok(supplyLine);
    }

    [HttpPost("post")]
    public IActionResult Post([FromBody] SupplyLine supplyLine)
    {
        if (supplyLine == null)
        {
            return BadRequest();
        }
        UpdateQuantityBooks.UpdateQuantityOnPost(supplyLine);
        _supplyLineDataAccess.AddSupplyLine(supplyLine);

        return CreatedAtAction(nameof(Get), new { id = supplyLine.supplyLineId }, supplyLine);
    }

    [HttpPut("{id}/update")]
    public IActionResult Put(Guid id, [FromBody] SupplyLine supplyLine)
    {
        if (supplyLine == null || supplyLine.supplyLineId != id)
        {
            return BadRequest();
        }

        var existingSupplyLine = _supplyLineDataAccess.GetSupplyLineById(id);
        var oldQuantity = existingSupplyLine.quantity;
        UpdateQuantityBooks.UpdateQuantityOnUpdate(oldQuantity, supplyLine);
        if (existingSupplyLine == null)
        {
            return NotFound();
        }

        _supplyLineDataAccess.UpdateSupplyLine(supplyLine);
        return NoContent();
    }

    [HttpDelete("{id}/delete")]
    public IActionResult Delete(Guid id)
    {
        var supplyLine = _supplyLineDataAccess.GetSupplyLineById(id);
        if (supplyLine == null)
        {
            return NotFound();
        }

        _supplyLineDataAccess.DeleteSupplyLine(id);
        return NoContent();
    }
}
