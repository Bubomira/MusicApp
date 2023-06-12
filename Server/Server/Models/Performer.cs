using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Performer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public ICollection<SecondaryPerformers> SecondaryPerformers { get; set; }

    }
}
