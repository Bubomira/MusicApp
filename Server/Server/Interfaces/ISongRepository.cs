using Server.Models;

namespace Server.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetSongs();

        Task<Song> GetSongById(int songId);

        Task<bool> CheckIfThereIsSongById(int songId);

    }
}
