using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string Notes { get; set; }

        public Performer Performer { get; set; }

        public ICollection<Song> Songs { get; set; }

        public ICollection<AlbumsUsers> AlbumsUsers { get; set; }
    }
}
