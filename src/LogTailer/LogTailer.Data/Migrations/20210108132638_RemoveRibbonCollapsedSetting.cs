using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class RemoveRibbonCollapsedSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) =>
            migrationBuilder.DropColumn(
                name: "RibbonCollapsed",
                table: "Sessions");

        protected override void Down(MigrationBuilder migrationBuilder) =>
            migrationBuilder.AddColumn<bool>(
                name: "RibbonCollapsed",
                table: "Sessions",
                type: "INTEGER",
                unicode: false,
                nullable: false,
                defaultValue: false);
    }
}
