using Microsoft.EntityFrameworkCore;
using Server.Data;
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

        public  Task<List<Song>> GetSongs()
        {
            return _musicDbContext.Songs.ToListAsync();
        }

        public Task<Song> GetSongById(int songId)
        {
            return _musicDbContext.Songs.Where(s => s.Id == songId)
                .Include(s=>s.Album)
                .Include(s=>s.Album.Performer)
                .Include(s=>s.SecondaryPerformers)
                .FirstOrDefaultAsync();
        }

        public bool CheckIfThereIsSongById(int songId)
        {
            return _musicDbContext.Songs.Any(s => s.Id == songId);
        }
    }
}
