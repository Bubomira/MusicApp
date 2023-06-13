using AutoMapper.Configuration.Annotations;
using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Server.DTO.SongDTO
{
    public class SongDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Notes { get; set; }
        public int ReleaseDate { get; set; }

        [SourceMember(nameof(Song.Album.Name))]
        public string AlbumName { get; set; }

        [SourceMember(nameof(Song.Album.Performer.Name))]
        public string PerformerName { get; set; }

        public ICollection<string>? SecondaryPerformers { get; set; }

    }
}
