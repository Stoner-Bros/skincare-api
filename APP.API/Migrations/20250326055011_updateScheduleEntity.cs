using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class updateScheduleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "work_date",
                schema: "dbo",
                table: "skin_therapist_schedule",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "work_date",
                schema: "dbo",
                table: "skin_therapist_schedule",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
