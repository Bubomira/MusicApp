using Server.Models;

namespace Server.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> RegisterUser(string username, string passwordHash);

        public Task<User> LoginUser(string username);

        public Task<bool> LogoutUser();

        public Task<bool> CheckIfUserExistsByUsername(string username);

    }
}
