using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class RemoveAllowFiltering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) =>
            migrationBuilder.DropColumn(
                name: "AllowFiltering",
                table: "Sessions");

        protected override void Down(MigrationBuilder migrationBuilder) =>
            migrationBuilder.AddColumn<bool>(
                name: "AllowFiltering",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
    }
}
