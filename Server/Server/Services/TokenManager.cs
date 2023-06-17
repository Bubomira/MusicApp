using Microsoft.IdentityModel.Tokens;
using Server.Interfaces;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly SymmetricSecurityKey _key;
        private readonly SigningCredentials _signingCredentials;
        private readonly IConfiguration _configuration;
        public TokenManager(IConfiguration config)
        {
            _configuration = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])) ;
            _signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        }
        public Task<string> CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddDays(2),
                signingCredentials:_signingCredentials);

           string verifyedToken= new JwtSecurityTokenHandler().WriteToken(token);

            return Task.Run(() => verifyedToken);

        }
    }
}
