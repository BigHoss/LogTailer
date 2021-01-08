using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class RemoveTheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) =>
            migrationBuilder.DropColumn(
                name: "ThemeIndex",
                table: "Sessions");

        protected override void Down(MigrationBuilder migrationBuilder) =>
            migrationBuilder.AddColumn<int>(
                name: "ThemeIndex",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
    }
}
