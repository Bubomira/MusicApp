using Server.Models;

namespace Server.Interfaces
{
    public interface IAlbumRepository
    {

        public Task<Album> GetAlbumById(int albumId);

        public void LikeAlbum(int userId, int albumId);
        public void DislikeAlbum(int userId, int albumId);

        public Task<bool> CheckIfAlbumExists(int albumId);

        public Task<bool> CheckIfAlbumIsLikedByUser(int userId,int albumId);

    }
}
