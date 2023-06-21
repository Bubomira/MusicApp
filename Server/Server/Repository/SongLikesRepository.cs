using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO.SongDTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class SongLikesRepository : ISongLikesRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public SongLikesRepository(MusicDbContext musicDbContext)
        {

            _musicDbContext = musicDbContext;

        }
        public async void LikeSong(int userId, int songId)
        { 
            var songToBeLiked = new SongsUsers()
            {
                SongId = songId,
                UserId = userId
            };
            _musicDbContext.SongsUsers.AddAsync(songToBeLiked);

            await _musicDbContext.SaveChangesAsync();
        }

        public async void DislikeSong(int userId, int songId)
        {
            var songToBeDisliked = _musicDbContext.SongsUsers
                .Where(su => su.SongId == songId && su.UserId == userId).FirstOrDefaultAsync();

            _musicDbContext.Remove(songToBeDisliked);

            await _musicDbContext.SaveChangesAsync();
        }

        public Task<bool> CheckIfUserHasLikedSong(int userId, int songId)
        {
           return _musicDbContext.SongsUsers.AnyAsync(su=>su.UserId==userId && su.SongId==songId);
        }
    }
}
