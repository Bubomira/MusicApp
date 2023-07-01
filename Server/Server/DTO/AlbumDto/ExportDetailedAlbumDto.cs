using Server.DTO.SongDTO;

namespace Server.DTO.AlbumDto
{
    public class ExportDetailedAlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PerformerName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Notes { get; set; }

        public List<NormalSongDto> Songs { get; set; }
    }
}
