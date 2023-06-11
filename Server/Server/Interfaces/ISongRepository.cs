using Server.Models;

namespace Server.Interfaces
{
    public interface ISongRepository
    {
        ICollection<Song> GetSongs();

    }
}
