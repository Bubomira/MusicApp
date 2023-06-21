using Server.DTO.PlaylistDto;
using Server.Models;

namespace Server.Interfaces
{
    public interface IPlaylistRepository
    {
        //probably will be moved to another repo
        Task<List<ExportDetailedPlaylistDto>> GetLikedPlaylists(int userId);

        Task<List<ExportDetailedPlaylistDto>> GetOwnedPlaylists(int userId);

        //like-dislike
        void LikePlaylist(int userId, int playlistId);

        void DislikePlaylist(int userId, int playlistId);

        //Crud
        Task<ExportDetailedPlaylistDto> GetPlaylistDetails(int playlistId);

        void CreatePlaylist(int userId,string playlistName);

       void UpdatePlaylist(int playlistId, string playlistName);
        void DeletePlaylist(int playlistId);

        //Checkers
        Task<bool> CheckIfPlaylistIsLikedByCurrentUser(int userId, int playlistId);

        Task<bool> CheckIfPlaylistIsOwnedByCurrentUser(int userId, int playlistId);

        Task<bool> CheckIfPlaylistExist(int playlistId);

        Task<Playlist> GetPlaylistById(int playlistId);


    }
}
