using Microsoft.AspNetCore.Mvc;
using BookStoreWebVue.Server.BookStore;
using BookStoreWebVue.Server.DataAccess;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using BookStoreWebVue.Server.Functions;


namespace BookStoreWebVue.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationsController : ControllerBase
    {
        private readonly UserDataAccess _userDataAccess;
        
        public AuthorizationsController(UserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var user = _userDataAccess.GetUserByEmail(request.Email);

            if (user == null || !VerifyPassword(request.Password, user.passwordHash))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            string token = AuthorizationFunc.GenerateJwtToken(user);

            return Ok(new { user, Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User request)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var existingUser = _userDataAccess.GetUserByEmail(request.email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            // Hash the password before storing it in the database
            string passwordHash = HashPassword(request.passwordHash);

            var newUser = new User
            {
                email = request.email,
                passwordHash = passwordHash,
                isAdmin = true,
                nickname = request.nickname,
            };

            _userDataAccess.AddUser(newUser);

            // Generate JWT token
            string token = AuthorizationFunc.GenerateJwtToken(newUser);

            // Return user data along with token
            return Ok(new { newUser, Token = token });
        }
        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            // Очищаем токен из Local Storage
            Response.Cookies.Delete("token");

            // Отправляем ответ об успешном выходе из системы
            return Ok(new { message = "User logged out successfully" });
        }
        // Method to hash the password
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedPasswordHash;
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
