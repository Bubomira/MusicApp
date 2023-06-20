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
        public Task<List<NormalSongDto>> GetLikedSongs(int userId)
        {
            return _musicDbContext.SongsUsers.Where(su => su.UserId == userId)
                .Select(su => new NormalSongDto()
                {
                    Name = su.Song.Name,
                    Id = su.Song.Id,
                    PerformerName = su.Song.Album.Performer.Name,
                    SecondaryPerformers = su.Song.SecondaryPerformers
                    .Select(sp => sp.Performer.Name).ToList()
                }
                )
                .ToListAsync();
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

        public void DislikeSong(int userId, int songId)
        {
            var songToBeDisliked = _musicDbContext.SongsUsers
                .Where(su => su.SongId == songId && su.UserId == userId).FirstOrDefaultAsync();

            _musicDbContext.Remove(songToBeDisliked);
        }

        public Task<bool> CheckIfUserHasLikedSong(int userId, int songId)
        {
           return _musicDbContext.SongsUsers.AnyAsync(su=>su.UserId==userId && su.SongId==songId);
        }
    }
}
