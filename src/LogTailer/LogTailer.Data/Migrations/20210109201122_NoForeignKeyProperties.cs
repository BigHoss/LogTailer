using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class NoForeignKeyProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineHighlights_Sessions_SessionId",
                table: "LineHighlights");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenFiles_Sessions_SessionId",
                table: "OpenFiles");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "OpenFiles",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "LineHighlights",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_LineHighlights_Sessions_SessionId",
                table: "LineHighlights",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenFiles_Sessions_SessionId",
                table: "OpenFiles",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineHighlights_Sessions_SessionId",
                table: "LineHighlights");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenFiles_Sessions_SessionId",
                table: "OpenFiles");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "OpenFiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "LineHighlights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LineHighlights_Sessions_SessionId",
                table: "LineHighlights",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenFiles_Sessions_SessionId",
                table: "OpenFiles",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
