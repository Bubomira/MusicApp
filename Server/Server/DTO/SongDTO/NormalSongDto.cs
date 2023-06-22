using AutoMapper.Configuration.Annotations;
using Server.Models;

namespace Server.DTO.SongDTO
{
    public class NormalSongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string>? Performers { get; set; }

    }
}
