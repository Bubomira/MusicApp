namespace Server.Interfaces
{
    public interface IPasswordHasher
    {
        public string CreatePasswordHash(string password);
    }
}
