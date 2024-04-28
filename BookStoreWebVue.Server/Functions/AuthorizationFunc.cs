using BookStoreWebVue.Server.BookStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookStoreWebVue.Server.Functions
{
    public class AuthorizationFunc
    {
        public static string GenerateJwtToken(User user)
        {
            var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("KeyStrings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("userId", user.userId.ToString()),
                    new Claim(ClaimTypes.Role, user.isAdmin ? "admin" : "user"),
                    new Claim("nickname", user.nickname),
                    new Claim(ClaimTypes.Name, user.firstName),
                    new Claim(ClaimTypes.Surname, user.lastName),
                    new Claim("addressId", user.customerAddress)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
