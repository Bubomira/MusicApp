using Server.Models;

namespace Server.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<Playlist>> GetLikedPlaylists(int userId);

        Task<List<Playlist>> GetOwnedPlaylists(int userId);

        Task<List<Song>> GetLikedSongs(int userId);
    }
}
