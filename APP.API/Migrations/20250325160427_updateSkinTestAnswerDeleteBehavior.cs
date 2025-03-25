using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class updateSkinTestAnswerDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skin_test_answer_skin_test_skin_test_id",
                schema: "dbo",
                table: "skin_test_answer");

            migrationBuilder.AddForeignKey(
                name: "FK_skin_test_answer_skin_test_skin_test_id",
                schema: "dbo",
                table: "skin_test_answer",
                column: "skin_test_id",
                principalSchema: "dbo",
                principalTable: "skin_test",
                principalColumn: "skin_test_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skin_test_answer_skin_test_skin_test_id",
                schema: "dbo",
                table: "skin_test_answer");

            migrationBuilder.AddForeignKey(
                name: "FK_skin_test_answer_skin_test_skin_test_id",
                schema: "dbo",
                table: "skin_test_answer",
                column: "skin_test_id",
                principalSchema: "dbo",
                principalTable: "skin_test",
                principalColumn: "skin_test_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
