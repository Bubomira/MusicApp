using AutoMapper.Configuration.Annotations;
using Server.Models;

namespace Server.DTO.SongDTO
{
    public class NormalSongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [SourceMember(nameof(Song.Album.Performer.Name))]
        public string PerformerName { get; set; }

        public ICollection<string>? SecondaryPerformers { get; set; }
    }
}
