using AutoMapper;
using Server.DTO.SongDTO;
using Server.Models;

namespace Server.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Song, SongDetailsDto>()
                .ForMember(s => s.PerformerName,
                   opt => opt.MapFrom(src =>
                   src.Album.Performer.Name))
                .ForMember(s => s.AlbumName,
                   opt => opt.MapFrom(src =>
                   src.Album.Name))
                .ForMember(s => s.SecondaryPerformers,
                     opt => opt.MapFrom(src =>
                     src.SecondaryPerformers.Select(x => x.Performer.Name).ToList()));
        }

    }
}
