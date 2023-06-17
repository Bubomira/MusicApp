using Microsoft.AspNetCore.Identity;
using Server.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Server.Services
{
    public class PasswordHasher:IPasswordHasher
    {

        public string CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(passwordHash);
            }
        }
    }
}
