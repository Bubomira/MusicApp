using AutoMapper;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public UserRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public Task<User> LoginUser()
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterUser()
        {
            throw new NotImplementedException();
        }
        public Task<bool> LogoutUser()
        {
            throw new NotImplementedException();
        }
        public Task<bool> CheckIfUserExists()
        {
            throw new NotImplementedException();
        }

    }
}
