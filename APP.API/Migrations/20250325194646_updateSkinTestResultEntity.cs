using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class updateSkinTestResultEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skin_test_result_skin_test_skin_test_id",
                schema: "dbo",
                table: "skin_test_result");

            migrationBuilder.DropIndex(
                name: "IX_skin_test_result_skin_test_id",
                schema: "dbo",
                table: "skin_test_result");

            migrationBuilder.RenameColumn(
                name: "skin_test_id",
                schema: "dbo",
                table: "skin_test_result",
                newName: "skin_test_answer_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                schema: "dbo",
                table: "skin_test_result",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "guest_id",
                schema: "dbo",
                table: "skin_test_result",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                schema: "dbo",
                table: "skin_test_result",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_result_skin_test_answer_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "skin_test_answer_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_skin_test_result_skin_test_answer_skin_test_answer_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "skin_test_answer_id",
                principalSchema: "dbo",
                principalTable: "skin_test_answer",
                principalColumn: "answer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skin_test_result_skin_test_answer_skin_test_answer_id",
                schema: "dbo",
                table: "skin_test_result");

            migrationBuilder.DropIndex(
                name: "IX_skin_test_result_skin_test_answer_id",
                schema: "dbo",
                table: "skin_test_result");

            migrationBuilder.RenameColumn(
                name: "skin_test_answer_id",
                schema: "dbo",
                table: "skin_test_result",
                newName: "skin_test_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                schema: "dbo",
                table: "skin_test_result",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "guest_id",
                schema: "dbo",
                table: "skin_test_result",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                schema: "dbo",
                table: "skin_test_result",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_result_skin_test_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "skin_test_id");

            migrationBuilder.AddForeignKey(
                name: "FK_skin_test_result_skin_test_skin_test_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "skin_test_id",
                principalSchema: "dbo",
                principalTable: "skin_test",
                principalColumn: "skin_test_id");
        }
    }
}
