using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public Task<User> LoginUser(string username)
        {
            return _musicDbContext.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> RegisterUser(string username,string passwordHash)
        {
            User user = new User();
            user.Username = username;
            user.PasswordHash = passwordHash;

            _musicDbContext.Users.AddAsync(user);
            await _musicDbContext.SaveChangesAsync();

            return user;
        }
        public Task<bool> LogoutUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfUserExistsByUsername(string username)
        {
            return _musicDbContext.Users.AnyAsync(x => x.Username == username);
        }
    }
}
