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
                 .Include(s => s.SecondaryPerformers)
                 .Include(s => s.Album.Performer)
                 .ToListAsync();
            
        }

        public Task<Song> GetSongById(int songId)
        {
            return _musicDbContext.Songs.Where(s => s.Id == songId)
                .Include(s=>s.Album)
                .Include(s=>s.Album.Performer)
                .Include(s=>s.SecondaryPerformers)
                .FirstOrDefaultAsync();
        }

        public Task<bool> CheckIfThereIsSongById(int songId)
        {
            return _musicDbContext.Songs.AnyAsync(s => s.Id == songId);
        }

        public Task<List<Song>> GetLikedSongs(int userId)
        {
            return _musicDbContext.SongsUsers.Where(su => su.UserId == userId)
                .Select(su => su.Song)
                 .Include(s => s.Album)
                .Include(s => s.Album.Performer)
                .Include(s => s.SecondaryPerformers)
                .ToListAsync();
        }
    }
}
