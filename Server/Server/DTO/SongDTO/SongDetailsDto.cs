using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Server.DTO.SongDTO
{
    public class SongDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Notes { get; set; }

        public string AlbumName { get; set; }

        public int ReleaseYear { get; set; }

        public string PerformerName { get; set; }

        public ICollection<string>? SecondaryPerformers { get; set; }

    }
}
