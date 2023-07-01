using Server.DTO.SongDTO;
using Server.Models;

namespace Server.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetWeeklySongs();

        Task<Song> GetSongById(int songId);

        Task<bool> CheckIfThereIsSongById(int songId);

        void LikeSong(int userId, int songId);

        void DislikeSong(int userId, int songId);

        Task<bool> CheckIfUserHasLikedSong(int userId, int songId);


    }
}
