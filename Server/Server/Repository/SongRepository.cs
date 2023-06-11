using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class SongRepository :ISongRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public SongRepository(MusicDbContext musicDbContext)
        {
            this._musicDbContext = musicDbContext;
        }

        public ICollection<Song> GetSongs()
        {
            return _musicDbContext.Songs.ToList();
        }
    }
}
