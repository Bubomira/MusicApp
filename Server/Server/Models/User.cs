using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        //liked albums
        public ICollection<AlbumsUsers> AlbumsUsers { get; set; }

        //liked songs
        public ICollection<SongsUsers> SongsUsers { get; set; }

        public ICollection<OwnedUserPlaylists> OwnedPlaylistsUsers { get; set; }

        public ICollection<LikedUserPlaylists> LikedPlaylistsUsers { get; set; }

    }
}
