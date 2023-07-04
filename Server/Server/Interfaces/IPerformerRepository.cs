using Server.Models;

namespace Server.Interfaces
{
    public interface IPerformerRepository
    {
        public Task<List<SongPerformers>> GetStandaloneSongsByPerformer(int performerId);
        public Task<List<Album>> GetAlbumsByPerformer(int performerId);
        public Task<Performer> GetPerformerById(int performerId);
        public Task<bool> CheckIfPerformerExists(int performerId);

    }
}
