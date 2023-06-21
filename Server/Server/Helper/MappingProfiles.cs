using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Server.DTO.PlaylistDto;
using Server.DTO.SongDTO;
using Server.Models;

namespace Server.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Song, SongDetailsDto>()
                .ForMember(sdto => sdto.PerformerName,
                   opt => opt.MapFrom(s => s.Album.Performer.Name))
                .ForMember(s => s.AlbumName,
                   opt => opt.MapFrom(s => s.Album.Name))
                .ForMember(s => s.SecondaryPerformers,
                opt => opt.MapFrom(src =>
                  src.SecondaryPerformers.Select(x => x.Performer.Name).ToList()));

            CreateMap<Song,NormalSongDto>()
                .ForMember(sdto=>sdto.PerformerName,
                opt=>opt.MapFrom(s=>s.Album.Performer.Name))
                .ForMember(s => s.SecondaryPerformers,
                opt => opt.MapFrom(src =>
                  src.SecondaryPerformers.Select(x => x.Performer.Name).ToList()));

            CreateMap<Playlist, ExportNormalPlaylistDto>()
                .ForMember(expd => expd.OwnerName,
                opt => opt.MapFrom(p => p.Owner.Username))
                  .ForMember(expd => expd.PlaylistId,
                opt => opt.MapFrom(p => p.Id))
                   .ForMember(expd => expd.PlaylistName,
                opt => opt.MapFrom(p => p.Name));

        }

    }
}
