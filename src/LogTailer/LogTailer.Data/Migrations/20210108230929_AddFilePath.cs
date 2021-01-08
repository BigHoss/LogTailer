using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class AddFilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) =>
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "OpenFiles",
                type: "TEXT",
                nullable: true);

        protected override void Down(MigrationBuilder migrationBuilder) =>
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "OpenFiles");
    }
}
