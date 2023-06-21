using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;
using Server.Interfaces;

namespace Server.Repository
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public ProfileRepository(MusicDbContext musicDbContext)
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
        public Task<List<ExportNormalPlaylistDto>> GetLikedPlaylists(int userId)
        {
            return _musicDbContext.LikedPlaylistsUsers.Where(lp => lp.LikerId == userId)
                 .Select(lp => new ExportNormalPlaylistDto()
                 {
                     PlaylistName = lp.LikedPlaylist.Name,
                     PlaylistId = lp.PlaylistId,
                     OwnerId =lp.LikerId,
                     OwnerName = lp.Liker.Username           
                 }).ToListAsync();

        }

        public Task<List<ExportNormalPlaylistDto>> GetOwnedPlaylists(int userId)
        {
            return _musicDbContext.Users.Where(u => u.Id == userId)
                .Select(u => u.OwnedPlaylists.Select(lp => new ExportNormalPlaylistDto()
                {
                    PlaylistName = lp.Name,
                    PlaylistId = lp.Id,
                    OwnerId = lp.OwnerId,
                    OwnerName = lp.Owner.Username
                }).ToList())
                .FirstOrDefaultAsync();           
        }
    }
}
