﻿using Microsoft.EntityFrameworkCore;
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
        public Task<ExportDetailedPlaylistDto> GetPlaylistDetails(int playlistId)
        {
            return _musicDbContext.Playlists.Where(p => p.Id == playlistId)
                .Select(p => new ExportDetailedPlaylistDto()
                {
                    Id = p.Id,
                    OwnerId = p.OwnerId,
                    Likes = p.Likes,
                    Name = p.Name,
                    OwnerName = p.Owner.Username,
                    Songs = p.SongsPlaylists
                           .Where(sp => sp.PlaylistId == playlistId)
                           .Select(sp => new DTO.SongDTO.NormalSongDto()
                           {
                               Id= sp.Song.Id,
                               Name = sp.Song.Name,
                               PerformerName = sp.Song.Album.Performer.Name,
                               SecondaryPerformers = sp.Song.SecondaryPerformers.Select(sp=>sp.Performer.Name).ToList()
                           }).ToList()
                })
               .FirstOrDefaultAsync();
        }
        public async void CreatePlaylist(int userId,string playlistName)
        {
            Playlist playlist = new Playlist()
            {
                Name = playlistName,
                Likes = 0,
                OwnerId = userId
            };

            _musicDbContext.Playlists.AddAsync(playlist);

            await _musicDbContext.SaveChangesAsync();

        }
        public async void UpdatePlaylist(int playlistId,string playlistName)
        {
            var playlist = await GetPlaylistById(playlistId);

            playlist.Name = playlistName;

           await _musicDbContext.SaveChangesAsync();
        }

        public async void DeletePlaylist(int playlistId,int ownerId)
        {
            var playlist = await GetPlaylistById(playlistId);

            _musicDbContext.Playlists.Remove(playlist);

            await _musicDbContext.SaveChangesAsync();

        }
        public void LikePlaylist(int userId, int playlistId)
        {
            throw new NotImplementedException();
        }

        public void DislikePlaylist(int userId, int playlistId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExportDetailedPlaylistDto>> GetLikedPlaylists(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExportDetailedPlaylistDto>> GetOwnedPlaylists(int userId)
        {
            throw new NotImplementedException();
        }



        public Task<bool> CheckIfPlaylistIsLikedByCurrentUser(int userId, int playlistId)
        {
            return _musicDbContext.LikedPlaylistsUsers.AnyAsync(pl => pl.PlaylistId == playlistId && pl.LikerId == userId);
        }

        public Task<bool> CheckIfPlaylistIsOwnedByCurrentUser(int userId, int playlistId)
        {
            throw new NotImplementedException();
            // return _musicDbContext.OwnedPlaylistsUsers.AnyAsync(pl => pl.PlaylistId == playlistId && pl.OwnerId == userId);
        }

        public Task<bool> CheckIfPlaylistExist(int playlistId)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> GetPlaylistById(int playlistId)
        {
            return _musicDbContext.Playlists.Where(p => p.Id == playlistId).FirstOrDefaultAsync();
        }
    }
}