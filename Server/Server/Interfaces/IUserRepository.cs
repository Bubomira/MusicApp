using Server.Models;

namespace Server.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> RegisterUser();

        public Task<User> LoginUser();

        public Task<bool> LogoutUser();

        public Task<bool> CheckIfUserExists();

    }
}
