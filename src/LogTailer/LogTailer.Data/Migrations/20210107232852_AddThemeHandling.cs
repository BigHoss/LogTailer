using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class AddThemeHandling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeIndex",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeIndex",
                table: "Sessions");
        }
    }
}
