using Server.Migrations;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Notes { get; set; }

        public int ReleaseDate { get; set; }

        public Album Album { get; set; }

        //liked songs
        public ICollection<SongsUsers> SongsUsers { get; set; }

        public ICollection<SongsPlaylists> SongsPlaylists { get; set; }

        public ICollection<SongPerformers> SongPerformers { get; set; }

    }
}
