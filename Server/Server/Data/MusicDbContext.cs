﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<SongsUsers> SongsUsers { get; set; }

        public DbSet<SongsPlaylists> SongsPlaylists { get; set; }

        public DbSet<LikedUserPlaylists> LikedPlaylistsUsers { get; set; }

        public DbSet<SongPerformers> SongPerformers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Owner)
                .WithMany(p => p.OwnedPlaylists)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
            //many-to-many with albums->users (liked albums)
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

            //many-to-many with songs->users (liked songs)

            modelBuilder.Entity<SongsUsers>()
                .HasKey(pc => new { pc.UserId, pc.SongId });

            modelBuilder.Entity<SongsUsers>()
                .HasOne(su => su.Song)
                .WithMany(su => su.SongsUsers)
                .HasForeignKey(su => su.SongId);

            modelBuilder.Entity<SongsUsers>()
                .HasOne(su => su.User)
                .WithMany(su => su.SongsUsers)
                .HasForeignKey(su => su.UserId);

            //many-to-many with songs->playlists 

            modelBuilder.Entity<SongsPlaylists>()
                .HasKey(pc => new { pc.SongId, pc.PlaylistId });

            modelBuilder.Entity<SongsPlaylists>()
                .HasOne(sp => sp.Song)
                .WithMany(sp => sp.SongsPlaylists)
                .HasForeignKey(sp => sp.SongId);

            modelBuilder.Entity<SongsPlaylists>()
                .HasOne(sp => sp.Playlist)
                .WithMany(sp=>sp.SongsPlaylists)
                .HasForeignKey(sp => sp.PlaylistId);

            //many-to-many with playlists->users (likedPlaylists)

            modelBuilder.Entity<LikedUserPlaylists>()
               .HasKey(pc => new { pc.LikerId, pc.PlaylistId });


            modelBuilder.Entity<LikedUserPlaylists>()
                .HasOne(pu => pu.Liker)
                .WithMany(pu => pu.LikedPlaylistsUsers)
                .HasForeignKey(pu => pu.LikerId);


            modelBuilder.Entity<LikedUserPlaylists>()
                .HasOne(pu => pu.LikedPlaylist)
                .WithMany(pu => pu.LikedPlaylistsUsers)
                .HasForeignKey(pu => pu.PlaylistId);

            //many-to-many with songs->performers

            modelBuilder.Entity<SongPerformers>()
                .HasKey(pc => new { pc.SongId, pc.PerformerId });

            modelBuilder.Entity<SongPerformers>()
                .HasOne(sp => sp.Song)
                .WithMany(sp => sp.SongPerformers)
                .HasForeignKey(sp => sp.SongId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SongPerformers>()
                .HasOne(sp => sp.Performer)
                .WithMany(sp => sp.SongPerformers)
                .HasForeignKey(sp => sp.PerformerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
