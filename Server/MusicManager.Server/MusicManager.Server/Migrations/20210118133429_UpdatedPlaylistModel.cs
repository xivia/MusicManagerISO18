using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicManager.Server.Migrations
{
    public partial class UpdatedPlaylistModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongToPlaylists");

            migrationBuilder.AddColumn<long>(
                name: "PlaylistId",
                table: "Songs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_PlaylistId",
                table: "Songs",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Playlists_PlaylistId",
                table: "Songs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Playlists_PlaylistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_PlaylistId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "SongToPlaylists",
                columns: table => new
                {
                    SongToPlaylistId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<bool>(nullable: false),
                    PlaylistId = table.Column<long>(nullable: true),
                    SongId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongToPlaylists", x => x.SongToPlaylistId);
                    table.ForeignKey(
                        name: "FK_SongToPlaylists_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongToPlaylists_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongToPlaylists_PlaylistId",
                table: "SongToPlaylists",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_SongToPlaylists_SongId",
                table: "SongToPlaylists",
                column: "SongId");
        }
    }
}
