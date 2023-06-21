using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;

namespace Server.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<ExportDetailedPlaylistDto>> GetLikedPlaylists(int userId);

        Task<List<ExportDetailedPlaylistDto>> GetOwnedPlaylists(int userId);

        Task<List<NormalSongDto>> GetLikedSongs(int userId);
    }
}
