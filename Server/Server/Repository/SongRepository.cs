using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO.SongDTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public SongRepository(MusicDbContext musicDbContext)
        {
            this._musicDbContext = musicDbContext;
        }

        public Task<List<Song>> GetWeeklySongs()
        {
            return _musicDbContext.Songs
                 .Take(10)
                 .Include(s => s.SongPerformers)
                 .ThenInclude(s => s.Performer)
                 .ToListAsync();
            
        }

        public Task<Song> GetSongById(int songId)
        {
            return _musicDbContext.Songs.Where(s => s.Id == songId)
                .Include(s=>s.Album)
                .Include(s=>s.SongPerformers)
                .ThenInclude(s => s.Performer)
                .FirstOrDefaultAsync();
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
            return _musicDbContext.SongsUsers.AnyAsync(su => su.UserId == userId && su.SongId == songId);
        }

        public Task<bool> CheckIfThereIsSongById(int songId)
        {
            return _musicDbContext.Songs.AnyAsync(s => s.Id == songId);
        }

    }
}
