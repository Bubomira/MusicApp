using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public int Likes { get; set; }

        public ICollection<SongsPlaylists> SongsPlaylists { get; set; }
        public User Owner { get; set; }

        public int OwnerId { get; set; }

        public ICollection<LikedUserPlaylists> LikedPlaylistsUsers { get; set; }
    }

}

