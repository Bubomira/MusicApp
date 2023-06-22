using Server.DTO.PlaylistDto;
using Server.Models;

namespace Server.Interfaces
{
    public interface IPlaylistRepository
    {

        //like-dislike
        void LikePlaylist(int userId, int playlistId);

        void DislikePlaylist(int userId, int playlistId);

        //Crud
       public Task<Playlist> GetPlaylistById(int playlistId);

        Task<Playlist> CreatePlaylist(int userId,string playlistName);

       void UpdatePlaylist(int playlistId, string playlistName);
        void DeletePlaylist(int playlistId);

        //Checkers
        Task<bool> CheckIfPlaylistIsLikedByCurrentUser(int userId, int playlistId);

        Task<bool> CheckIfPlaylistIsOwnedByCurrentUser(int userId, int playlistId);

        Task<bool> CheckIfPlaylistExist(int playlistId);


    }
}
