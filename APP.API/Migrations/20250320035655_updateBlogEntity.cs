using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class updateBlogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_treatment_result_booking_booking_id",
                schema: "dbo",
                table: "treatment_result");

            migrationBuilder.AddColumn<string>(
                name: "service_thumbnail_url",
                schema: "dbo",
                table: "service",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_treatment_result_booking_booking_id",
                schema: "dbo",
                table: "treatment_result",
                column: "booking_id",
                principalSchema: "dbo",
                principalTable: "booking",
                principalColumn: "booking_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_treatment_result_booking_booking_id",
                schema: "dbo",
                table: "treatment_result");

            migrationBuilder.DropColumn(
                name: "service_thumbnail_url",
                schema: "dbo",
                table: "service");

            migrationBuilder.AddForeignKey(
                name: "FK_treatment_result_booking_booking_id",
                schema: "dbo",
                table: "treatment_result",
                column: "booking_id",
                principalSchema: "dbo",
                principalTable: "booking",
                principalColumn: "booking_id");
        }
    }
}
