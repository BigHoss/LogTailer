using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class FixTypoForegroundColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForeGroundColor",
                table: "LineHighlights",
                newName: "ForegroundColor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForegroundColor",
                table: "LineHighlights",
                newName: "ForeGroundColor");
        }
    }
}
