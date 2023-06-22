using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO.PlaylistDto;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public PlaylistRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public Task<Playlist> GetPlaylistById(int playlistId)
        {
            return _musicDbContext.Playlists.Where(p => p.Id == playlistId)
                .Include(p=>p.Owner)
                .Include(p=>p.SongsPlaylists)
                .ThenInclude(sp=>sp.Song)
                .ThenInclude(s=>s.SongPerformers)      
                .ThenInclude(s=>s.Performer)
                .FirstAsync();
        }
       
        public async Task<Playlist> CreatePlaylist(int userId,string playlistName)
        {
            Playlist playlist = new Playlist()
            {
                Name = playlistName,
                Likes = 0,
                OwnerId = userId
            };

            _musicDbContext.Playlists.AddAsync(playlist);

            await _musicDbContext.SaveChangesAsync();

            return playlist;

        }
        public async void UpdatePlaylist(int playlistId,string playlistName)
        {
            var playlist = await GetPlaylistById(playlistId);

            playlist.Name = playlistName;

           await _musicDbContext.SaveChangesAsync();
        }

        public async void DeletePlaylist(int playlistId)
        {
            var playlist = await GetPlaylistById(playlistId);

            _musicDbContext.Playlists.Remove(playlist);

           await _musicDbContext.SaveChangesAsync();

        }
        public async void LikePlaylist(int userId, int playlistId)
        {
            var userPlaylist = new LikedUserPlaylists()
            {
                LikerId = userId,
                PlaylistId = playlistId
            };
            _musicDbContext.LikedPlaylistsUsers.AddAsync(userPlaylist);

            await _musicDbContext.SaveChangesAsync();
        }

        public async void DislikePlaylist(int userId, int playlistId)
        {
            var playlistToBeDisliked = await _musicDbContext.LikedPlaylistsUsers
                .Where(lp => lp.LikerId == userId && lp.PlaylistId == playlistId)
                .FirstOrDefaultAsync();

            _musicDbContext.LikedPlaylistsUsers.Remove(playlistToBeDisliked);

            await _musicDbContext.SaveChangesAsync();
        }

        public Task<bool> CheckIfPlaylistIsLikedByCurrentUser(int userId, int playlistId)
        {
            return _musicDbContext.LikedPlaylistsUsers.AnyAsync(pl => pl.PlaylistId == playlistId && pl.LikerId == userId);
        }

        public Task<bool> CheckIfPlaylistIsOwnedByCurrentUser(int userId, int playlistId)
        {
            return _musicDbContext.Playlists.AnyAsync(p => p.Id == playlistId && p.OwnerId == userId);
        }

        public Task<bool> CheckIfPlaylistExist(int playlistId)
        {
            return _musicDbContext.Playlists.AnyAsync(p => p.Id == playlistId);
        }

    }
}
