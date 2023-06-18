using Server.DTO.SongDTO;
using Server.Models;
namespace Server.Interfaces
{
    public interface ISongLikesRepository
    {
        Task<List<NormalSongDto>> GetLikedSongs(int userId);

        Task<Song> LikeSong(int userId,int songId);

        Task<Song> UnlikeSong(int userId,int songId);
    }
}
