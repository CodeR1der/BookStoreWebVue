using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using BookStoreWebVue.Server.Functions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDataAccess _userDataAccess;

        public UsersController(UserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allUsers = _userDataAccess.GetUsers();
            return Ok(allUsers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = _userDataAccess.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userDataAccess.AddUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.userId }, user);
        }


        [HttpPut("{id}/update")]
        public IActionResult Put(Guid id, [FromBody] User user)
        {
            if (user == null || user.userId != id)
            {
                return BadRequest();
            }

            var existingUser = _userDataAccess.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            
            _userDataAccess.UpdateUser(user);
            return NoContent();
        }

        [HttpPatch("{id}/patch")]
        public IActionResult Patch(Guid id, [FromBody] JsonPatchDocument<User> patchUser)
        {
            if (patchUser == null)
            {
                return BadRequest();
            }

            var existingUser = _userDataAccess.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            patchUser.ApplyTo(existingUser, ModelState);

            if (!TryValidateModel(existingUser))
            {
                return BadRequest(ModelState);
            }

            _userDataAccess.UpdateUser(existingUser);
            var token = AuthorizationFunc.GenerateJwtToken(existingUser);
            return Ok(new { Token = token });
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete(Guid id)
        {
            var user = _userDataAccess.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userDataAccess.DeleteUser(id);
            return NoContent();
        }
    }
}