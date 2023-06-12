using Server.Models;

namespace Server.Interfaces
{
    public interface ISongRepository
    {
        ICollection<Song> GetSongs();

        Song GetSongById(int songId);

        bool CheckIfThereIsSongById(int songId);

    }
}
