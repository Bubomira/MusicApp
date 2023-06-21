using Server.DTO.SongDTO;
using Server.Models;
namespace Server.Interfaces
{
    public interface ISongLikesRepository
    {

        void LikeSong(int userId,int songId);

        void DislikeSong(int userId,int songId);

        Task<bool> CheckIfUserHasLikedSong(int userId,int songId);
    }
}
