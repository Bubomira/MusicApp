using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class likedplaylistsrrlationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedPlaylistsUsers_Users_UserId",
                table: "OwnedPlaylistsUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OwnedPlaylistsUsers",
                newName: "OwnerId");

            migrationBuilder.CreateTable(
                name: "LikedUserPlaylists",
                columns: table => new
                {
                    LikerId = table.Column<int>(type: "int", nullable: false),
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedUserPlaylists", x => new { x.LikerId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_LikedUserPlaylists_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedUserPlaylists_Users_LikerId",
                        column: x => x.LikerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedUserPlaylists_PlaylistId",
                table: "LikedUserPlaylists",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedPlaylistsUsers_Users_OwnerId",
                table: "OwnedPlaylistsUsers",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedPlaylistsUsers_Users_OwnerId",
                table: "OwnedPlaylistsUsers");

            migrationBuilder.DropTable(
                name: "LikedUserPlaylists");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "OwnedPlaylistsUsers",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedPlaylistsUsers_Users_UserId",
                table: "OwnedPlaylistsUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
