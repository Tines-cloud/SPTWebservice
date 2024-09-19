using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPTKnowledgeService.Migrations
{
    /// <inheritdoc />
    public partial class CreateBreak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Break",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudySessionId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Break", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Break_StudySession_StudySessionId",
                        column: x => x.StudySessionId,
                        principalTable: "StudySession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreakDuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakDuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakDuration_Break_BreakId",
                        column: x => x.BreakId,
                        principalTable: "Break",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Break_StudySessionId",
                table: "Break",
                column: "StudySessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BreakDuration_BreakId",
                table: "BreakDuration",
                column: "BreakId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakDuration");

            migrationBuilder.DropTable(
                name: "Break");
        }
    }
}
