using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public double Duration { get => 34; }

        public int Likes { get; set; }

        public ICollection<SongsPlaylists> SongsPlaylists { get; set; }

    }
}
