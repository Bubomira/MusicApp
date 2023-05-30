namespace Server.Models
{
    public class OwnedUserPlaylists
    {
        public int OwnerId { get; set; }

        public int PlaylistId { get; set; }

        public User Owner { get; set; }

        public Playlist OwnedPlaylist { get; set; }
    }
}
