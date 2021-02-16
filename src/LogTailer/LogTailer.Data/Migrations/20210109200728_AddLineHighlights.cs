using Microsoft.EntityFrameworkCore.Migrations;

namespace LogTailer.Data.Migrations
{
    public partial class AddLineHighlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineHighlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SearchString = table.Column<string>(type: "TEXT", nullable: true),
                    ForeGroundColor = table.Column<int>(type: "INTEGER", nullable: false),
                    BackgroundColor = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRegex = table.Column<bool>(type: "INTEGER", nullable: false),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineHighlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineHighlights_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineHighlights_SessionId",
                table: "LineHighlights",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) =>
            migrationBuilder.DropTable(
                name: "LineHighlights");
    }
}
