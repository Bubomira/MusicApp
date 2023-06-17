using Server.Models;

namespace Server.Interfaces
{
    public interface ITokenManager
    {
        public Task<string> CreateToken(User user);
    }
}
