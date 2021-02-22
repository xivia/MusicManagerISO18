using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicManager.Server.Migrations
{
    public partial class AddedCoverFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverFilePath",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverFilePath",
                table: "Songs");
        }
    }
}
