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
        public string PasswordHash { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        public ICollection<Playlist> OwnedPlaylists { get; set; }
        //liked albums
        public ICollection<AlbumsUsers> AlbumsUsers { get; set; }

        //liked songs
        public ICollection<SongsUsers> SongsUsers { get; set; }


        public ICollection<LikedUserPlaylists> LikedPlaylistsUsers { get; set; }

    }
}
