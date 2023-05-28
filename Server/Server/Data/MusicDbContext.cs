using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Server.Models;

namespace Server.Data
{
    public class MusicDbContext:DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options):base(options)
        {
            
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Performer> Performers { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AlbumsUsers> AlbumsUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumsUsers>()
                .HasKey(pc => new { pc.UserId, pc.AlbumId });

            modelBuilder.Entity<AlbumsUsers>()
                .HasOne(au => au.User)
                .WithMany(au => au.AlbumsUsers)
                .HasForeignKey(au => au.UserId);

            modelBuilder.Entity<AlbumsUsers>()
                .HasOne(au => au.Album)
                .WithMany(au => au.AlbumsUsers)
                .HasForeignKey(au => au.AlbumId);

        }
    }
}
