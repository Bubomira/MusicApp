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
                .ForMember(s => s.Performers,
                opt => opt.MapFrom(src =>
                  src.SongPerformers.Select(x => x.Performer.Name).ToList()))
                .ForMember(s => s.MainPerformerId,
                opt => opt.MapFrom(src =>
                src.SongPerformers.Select(s => s.PerformerId).FirstOrDefault()));

            CreateMap<Song,NormalSongDto>()
                .ForMember(s => s.Performers,
                opt => opt.MapFrom(src =>
                  src.SongPerformers.Select(x => x.Performer.Name).ToList()));

            CreateMap<Playlist, ExportNormalPlaylistDto>()
                .ForMember(expd => expd.OwnerName,
                opt => opt.MapFrom(p => p.Owner.Username))
                  .ForMember(expd => expd.PlaylistId,
                opt => opt.MapFrom(p => p.Id))
                   .ForMember(expd => expd.PlaylistName,
                opt => opt.MapFrom(p => p.Name));

            CreateMap<Playlist, ExportDetailedPlaylistDto>()
              .ForMember(expd => expd.OwnerName,
              opt => opt.MapFrom(p => p.Owner.Username))
               .ForMember(expd => expd.Songs,
              opt => opt.MapFrom(s => s.SongsPlaylists
              .Select(sp => new NormalSongDto() {
                  Id = sp.SongId,
                  Name = sp.Song.Name,
                  Performers = sp.Song.SongPerformers.Select(ps => ps.Performer.Name).ToList(),
              }).ToList()));


        }

    }
}
