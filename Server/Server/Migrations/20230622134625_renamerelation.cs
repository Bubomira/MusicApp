﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class renamerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecondaryPerformers");

            migrationBuilder.CreateTable(
                name: "SongPerformers",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongPerformers", x => new { x.SongId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_SongPerformers_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongPerformers_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongPerformers_PerformerId",
                table: "SongPerformers",
                column: "PerformerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongPerformers");

            migrationBuilder.CreateTable(
                name: "SecondaryPerformers",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryPerformers", x => new { x.SongId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_SecondaryPerformers_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecondaryPerformers_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryPerformers_PerformerId",
                table: "SecondaryPerformers",
                column: "PerformerId");
        }
    }
}
