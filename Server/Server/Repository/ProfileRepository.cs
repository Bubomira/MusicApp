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
               .Include(su => su.Song.Album.Performer)
                .ToListAsync();
        }
        public Task<List<LikedUserPlaylists>> GetLikedPlaylists(int userId)
        {
            return _musicDbContext.LikedPlaylistsUsers.Where(lp => lp.LikerId == userId)
                .Include(lp=>lp.LikedPlaylist)
                .Include(lp=>lp.Liker)
                .ToListAsync();

        }

        public Task<List<Playlist>> GetOwnedPlaylists(int userId)
        {
            return _musicDbContext.Playlists
                 .Where(p => p.OwnerId == userId)
                 .Include(p => p.Owner)
                 .ToListAsync();
        }

    }
}
