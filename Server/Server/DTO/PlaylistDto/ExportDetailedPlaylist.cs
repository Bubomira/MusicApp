using Server.DTO.SongDTO;

namespace Server.DTO.PlaylistDto
{
    public class ExportDetailedPlaylist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Likes { get; set; }

        public List<NormalSongDto> Songs { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }
    }
}
