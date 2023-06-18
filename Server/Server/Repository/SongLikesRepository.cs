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
                        .Select(sp=>sp.Performer.Name).ToList()
                    }
                    )
                    .ToListAsync();
        }

        public Task<Song> LikeSong(int userId, int songId)
        {
            throw new NotImplementedException();
        }

        public Task<Song> UnlikeSong(int userId, int songId)
        {
            throw new NotImplementedException();
        }
    }
}
