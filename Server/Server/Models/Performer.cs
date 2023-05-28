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

    }
}
