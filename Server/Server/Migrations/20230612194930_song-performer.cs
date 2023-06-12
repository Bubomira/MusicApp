using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class songperformer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedUserPlaylists_Playlists_PlaylistId",
                table: "LikedUserPlaylists");

            migrationBuilder.DropForeignKey(
                name: "FK_LikedUserPlaylists_Users_LikerId",
                table: "LikedUserPlaylists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedUserPlaylists",
                table: "LikedUserPlaylists");

            migrationBuilder.RenameTable(
                name: "LikedUserPlaylists",
                newName: "LikedPlaylistsUsers");

            migrationBuilder.RenameIndex(
                name: "IX_LikedUserPlaylists_PlaylistId",
                table: "LikedPlaylistsUsers",
                newName: "IX_LikedPlaylistsUsers_PlaylistId");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseDate",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedPlaylistsUsers",
                table: "LikedPlaylistsUsers",
                columns: new[] { "LikerId", "PlaylistId" });

            migrationBuilder.CreateTable(
                name: "SongsPerformers",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongsPerformers", x => new { x.SongId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_SongsPerformers_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongsPerformers_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongsPerformers_PerformerId",
                table: "SongsPerformers",
                column: "PerformerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedPlaylistsUsers_Playlists_PlaylistId",
                table: "LikedPlaylistsUsers",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedPlaylistsUsers_Users_LikerId",
                table: "LikedPlaylistsUsers",
                column: "LikerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedPlaylistsUsers_Playlists_PlaylistId",
                table: "LikedPlaylistsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LikedPlaylistsUsers_Users_LikerId",
                table: "LikedPlaylistsUsers");

            migrationBuilder.DropTable(
                name: "SongsPerformers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedPlaylistsUsers",
                table: "LikedPlaylistsUsers");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Songs");

            migrationBuilder.RenameTable(
                name: "LikedPlaylistsUsers",
                newName: "LikedUserPlaylists");

            migrationBuilder.RenameIndex(
                name: "IX_LikedPlaylistsUsers_PlaylistId",
                table: "LikedUserPlaylists",
                newName: "IX_LikedUserPlaylists_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedUserPlaylists",
                table: "LikedUserPlaylists",
                columns: new[] { "LikerId", "PlaylistId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LikedUserPlaylists_Playlists_PlaylistId",
                table: "LikedUserPlaylists",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedUserPlaylists_Users_LikerId",
                table: "LikedUserPlaylists",
                column: "LikerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
