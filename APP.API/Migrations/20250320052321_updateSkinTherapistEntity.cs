using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class updateSkinTherapistEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "yoe",
                schema: "dbo",
                table: "skin_therapist");

            migrationBuilder.AddColumn<string>(
                name: "experience",
                schema: "dbo",
                table: "skin_therapist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "introduction",
                schema: "dbo",
                table: "skin_therapist",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "experience",
                schema: "dbo",
                table: "skin_therapist");

            migrationBuilder.DropColumn(
                name: "introduction",
                schema: "dbo",
                table: "skin_therapist");

            migrationBuilder.AddColumn<int>(
                name: "yoe",
                schema: "dbo",
                table: "skin_therapist",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
