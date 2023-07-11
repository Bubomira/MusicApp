using Server.DTO.SongDTO;

namespace Server.DTO.PerformerDto
{
    public class PerformerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public List<PopularSongDto> PopularSongs { get; set; }
    }
}
