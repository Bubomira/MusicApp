using Server.DTO.SongDTO;
using Server.Models;

namespace Server.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetWeeklySongs();

        Task<Song> GetSongById(int songId);

        Task<bool> CheckIfThereIsSongById(int songId);



    }
}
