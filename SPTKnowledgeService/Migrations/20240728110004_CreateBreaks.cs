using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPTKnowledgeService.Migrations
{
    /// <inheritdoc />
    public partial class CreateBreaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Break_StudySession_StudySessionId",
                table: "Break");

            migrationBuilder.DropForeignKey(
                name: "FK_BreakDuration_Break_BreakId",
                table: "BreakDuration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Break",
                table: "Break");

            migrationBuilder.RenameTable(
                name: "Break",
                newName: "Breaks");

            migrationBuilder.RenameIndex(
                name: "IX_Break_StudySessionId",
                table: "Breaks",
                newName: "IX_Breaks_StudySessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakDuration_Breaks_BreakId",
                table: "BreakDuration",
                column: "BreakId",
                principalTable: "Breaks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Breaks_StudySession_StudySessionId",
                table: "Breaks",
                column: "StudySessionId",
                principalTable: "StudySession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakDuration_Breaks_BreakId",
                table: "BreakDuration");

            migrationBuilder.DropForeignKey(
                name: "FK_Breaks_StudySession_StudySessionId",
                table: "Breaks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Breaks",
                table: "Breaks");

            migrationBuilder.RenameTable(
                name: "Breaks",
                newName: "Break");

            migrationBuilder.RenameIndex(
                name: "IX_Breaks_StudySessionId",
                table: "Break",
                newName: "IX_Break_StudySessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Break",
                table: "Break",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Break_StudySession_StudySessionId",
                table: "Break",
                column: "StudySessionId",
                principalTable: "StudySession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BreakDuration_Break_BreakId",
                table: "BreakDuration",
                column: "BreakId",
                principalTable: "Break",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
