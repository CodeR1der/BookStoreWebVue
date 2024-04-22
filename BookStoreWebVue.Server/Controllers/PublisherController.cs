using BookStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TestOperations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherDataAccess _publisherDataAccess;

        public PublishersController(PublisherDataAccess publisherDataAccess)
        {
            _publisherDataAccess = publisherDataAccess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allPublishers = _publisherDataAccess.GetPublishers();
            return Ok(allPublishers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var publisher = _publisherDataAccess.GetPublisherById(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest();
            }

            _publisherDataAccess.AddPublisher(publisher);
            return CreatedAtAction(nameof(Get), new { id = publisher.publisherId }, publisher);
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int id, [FromBody] Publisher publisher)
        {
            if (publisher == null || publisher.publisherId != id)
            {
                return BadRequest();
            }

            var existingPublisher = _publisherDataAccess.GetPublisherById(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }

            _publisherDataAccess.UpdatePublisher(publisher);
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            var publisher = _publisherDataAccess.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _publisherDataAccess.DeletePublisher(id);
            return NoContent();
        }
    }
}
