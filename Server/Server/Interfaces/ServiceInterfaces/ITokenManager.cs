using Server.Models;

namespace Server.Interfaces.ServiceInterfaces
{
    public interface ITokenManager
    {
        public Task<string> CreateToken(User user);
    }
}
