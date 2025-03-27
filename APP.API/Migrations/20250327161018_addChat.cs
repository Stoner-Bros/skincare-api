using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class addChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "threads",
                schema: "dbo",
                columns: table => new
                {
                    thread_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    staff_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_threads", x => x.thread_id);
                    table.ForeignKey(
                        name: "FK_threads_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_threads_staff_staff_id",
                        column: x => x.staff_id,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "message",
                schema: "dbo",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thread_id = table.Column<int>(type: "int", nullable: false),
                    sender_id = table.Column<int>(type: "int", nullable: false),
                    sender_role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.message_id);
                    table.ForeignKey(
                        name: "FK_message_threads_thread_id",
                        column: x => x.thread_id,
                        principalSchema: "dbo",
                        principalTable: "threads",
                        principalColumn: "thread_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_message_thread_id",
                schema: "dbo",
                table: "message",
                column: "thread_id");

            migrationBuilder.CreateIndex(
                name: "IX_threads_customer_id",
                schema: "dbo",
                table: "threads",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_threads_staff_id",
                schema: "dbo",
                table: "threads",
                column: "staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "message",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "threads",
                schema: "dbo");
        }
    }
}
