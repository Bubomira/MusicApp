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

        public Album Album { get; set; }

    }
}
