namespace Server.Models
{
    public class LikedUserPlaylists
    {
        public int LikerId { get; set; }

        public int PlaylistId { get; set; }

        public User Liker { get; set; }

        public Playlist LikedPlaylist { get; set; }
    }
}
