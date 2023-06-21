using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;

namespace Server.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<ExportNormalPlaylistDto>> GetLikedPlaylists(int userId);

        Task<List<ExportNormalPlaylistDto>> GetOwnedPlaylists(int userId);

        Task<List<NormalSongDto>> GetLikedSongs(int userId);
    }
}
