namespace Server.Models
{
    public class SongPerformers
    {
        public int SongId { get; set; }

        public Song Song { get; set; }

        public int PerformerId { get; set; }

        public Performer Performer { get; set; }
    }
}
