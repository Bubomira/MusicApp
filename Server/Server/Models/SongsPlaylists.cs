namespace Server.Models
{
    public class SongsPlaylists
    {

        public int SongId { get; set; }

        public int PlaylistId { get; set; }

        public Song Song { get; set; }

        public Playlist Playlist { get; set; }
    }
}
