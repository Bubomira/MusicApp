using Server.Models;

namespace Server.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<LikedUserPlaylists>> GetLikedPlaylists(int userId);

        Task<List<Playlist>> GetOwnedPlaylists(int userId);

        Task<List<SongsUsers>> GetLikedSongs(int userId);

        Task<List<AlbumsUsers>> GetLikedAlbums(int userId);
    }
}
