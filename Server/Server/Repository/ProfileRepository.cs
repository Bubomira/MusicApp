using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public ProfileRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public Task<List<SongsUsers>> GetLikedSongs(int userId)
        {
            return _musicDbContext.SongsUsers.Where(su => su.UserId == userId)
                .Include(su => su.Song)
               .Include(su => su.Song.SongPerformers)
                .ToListAsync();
        }
        public Task<List<LikedUserPlaylists>> GetLikedPlaylists(int userId)
        {
            return _musicDbContext.LikedPlaylistsUsers.Where(lp => lp.LikerId == userId)
                .Include(lp=>lp.LikedPlaylist)
                .ToListAsync();

        }

        public Task<List<Playlist>> GetOwnedPlaylists(int userId)
        {
            return _musicDbContext.Playlists
                 .Where(p => p.OwnerId == userId)
                 .ToListAsync();
        }

        public Task<List<AlbumsUsers>> GetLikedAlbums(int userId)
        {
            return _musicDbContext.AlbumsUsers
                 .Where(au => au.UserId == userId)
                 .Include(au => au.Album)
                 .ThenInclude(a=>a.Performer)
                 .ToListAsync();
        }
    }
}
