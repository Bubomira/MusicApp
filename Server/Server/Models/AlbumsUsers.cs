namespace Server.Models
{
    public class AlbumsUsers
    {
        //to do: composite key!
        public int AlbumId { get; set; }
        public int UserId { get; set; }

        public Album Album { get; set; }
        public User User { get; set; }
    }
}
