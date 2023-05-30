namespace Server.Models
{
    public class PlaylistsUsers
    {
        public int UserId { get; set; }

        public int PlaylistId { get; set; }

        public User User { get; set; }

        public Playlist Playlist { get; set; }
    }
}
