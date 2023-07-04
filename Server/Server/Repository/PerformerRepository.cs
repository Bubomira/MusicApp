using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class PerformerRepository : IPerformerRepository
    {
        private readonly MusicDbContext _musicDbContext;

        public PerformerRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public Task<bool> CheckIfPerformerExists(int performerId)
        {
            return _musicDbContext.Performers.AnyAsync(p => p.Id == performerId);
        }

        public Task<List<Album>> GetAlbumsByPerformer(int performerId)
        {
            return _musicDbContext.Albums.Where(a => a.Performer.Id == performerId)
                .ToListAsync();
        }

        public Task<Performer> GetPerformerById(int performerId)
        {
            return _musicDbContext.Performers.Where(p => p.Id == performerId)
                .FirstOrDefaultAsync();
        }

        //to be reviewed!
        public Task<List<SongPerformers>> GetStandaloneSongsByPerformer(int performerId)
        {
            return _musicDbContext.SongPerformers
                .Where(sp => sp.Performer.Id == performerId &&
                ! sp.Performer.Albums.SelectMany(a => a.Songs.Select(s => s.Id))
                 .Contains(sp.SongId))
                .ToListAsync();
        }
    }
}
