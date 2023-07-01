using System.ComponentModel.DataAnnotations;

namespace Server.DTO.AlbumDto
{
    public class ExportNormalAlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PerformerName { get; set; }

    }
}
