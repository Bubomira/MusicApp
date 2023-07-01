using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicDbContext _musicDbContext;
        public AlbumRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public Task<Album> GetAlbumById(int albumId)
        {
            return _musicDbContext.Albums.Where(a=>a.Id==albumId)
                .FirstOrDefaultAsync();
        }

        public async void LikeAlbum(int userId, int albumId)
        {
            var albumLike = new AlbumsUsers()
            {
                AlbumId = albumId,
                UserId = userId
            };
            _musicDbContext.AlbumsUsers.AddAsync(albumLike);

            await _musicDbContext.SaveChangesAsync();
        }

        public async void DislikeAlbum(int userId, int albumId)
        {
            var albumLike = _musicDbContext.AlbumsUsers.Where(au => au.UserId == userId && au.AlbumId == albumId)
                .First();

            _musicDbContext.AlbumsUsers.Remove(albumLike);

            await _musicDbContext.SaveChangesAsync();
        }

        public Task<bool> CheckIfAlbumIsLikedByUser(int userId, int albumId)
        {
            return _musicDbContext.AlbumsUsers.AnyAsync(au => au.UserId == userId && au.AlbumId == albumId);
        }
        public Task<bool> CheckIfAlbumExists(int albumId)
        {
            return _musicDbContext.Albums.AnyAsync(a => a.Id == albumId);
        }


    }
}
