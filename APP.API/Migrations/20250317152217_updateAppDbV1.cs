using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class updateAppDbV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_account_info",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropIndex(
                name: "IX_account_info_account_id",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Fullname_Length",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "account_info_id",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropColumn(
                name: "background",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropColumn(
                name: "avatar",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "full_name",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "dbo",
                table: "account");

            migrationBuilder.DropColumn(
                name: "last_login",
                schema: "dbo",
                table: "account");

            migrationBuilder.RenameColumn(
                name: "bio",
                schema: "dbo",
                table: "account_info",
                newName: "avatar");

            migrationBuilder.RenameColumn(
                name: "created_date",
                schema: "dbo",
                table: "account",
                newName: "updated_at");

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                schema: "dbo",
                table: "account_info",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "dbo",
                table: "account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_account_info",
                schema: "dbo",
                table: "account_info",
                column: "account_id");

            migrationBuilder.CreateTable(
                name: "blog",
                schema: "dbo",
                columns: table => new
                {
                    blog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    publish_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    view_count = table.Column<int>(type: "int", nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.blog_id);
                    table.ForeignKey(
                        name: "FK_blog_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking",
                schema: "dbo",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    treatment_id = table.Column<int>(type: "int", nullable: false),
                    skin_therapist_id = table.Column<int>(type: "int", nullable: false),
                    staff_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    guest_id = table.Column<int>(type: "int", nullable: false),
                    booking_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    checkin_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    checkout_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.booking_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    last_visit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_customer_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                schema: "dbo",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback", x => x.feedback_id);
                });

            migrationBuilder.CreateTable(
                name: "feedback_reply",
                schema: "dbo",
                columns: table => new
                {
                    feedback_reply_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staff_id = table.Column<int>(type: "int", nullable: false),
                    feedback_id = table.Column<int>(type: "int", nullable: false),
                    reply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback_reply", x => x.feedback_reply_id);
                });

            migrationBuilder.CreateTable(
                name: "guest",
                schema: "dbo",
                columns: table => new
                {
                    guest_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guest", x => x.guest_id);
                });

            migrationBuilder.CreateTable(
                name: "message",
                schema: "dbo",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id = table.Column<int>(type: "int", nullable: false),
                    receiver_id = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.message_id);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                schema: "dbo",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_notification_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                schema: "dbo",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transaction_id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "service",
                schema: "dbo",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    service_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.service_id);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    centre_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    centre_address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    centre_phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    centre_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    opening_hours = table.Column<TimeSpan>(type: "time", nullable: false),
                    closing_hours = table.Column<TimeSpan>(type: "time", nullable: false),
                    opening_days = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "skin_test",
                schema: "dbo",
                columns: table => new
                {
                    skin_test_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skin_test", x => x.skin_test_id);
                });

            migrationBuilder.CreateTable(
                name: "skin_test_question",
                schema: "dbo",
                columns: table => new
                {
                    skin_test_question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skin_test_id = table.Column<int>(type: "int", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    option_a = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    option_b = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    option_c = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    option_d = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skin_test_question", x => x.skin_test_question_id);
                });

            migrationBuilder.CreateTable(
                name: "skin_test_result",
                schema: "dbo",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    guest_id = table.Column<int>(type: "int", nullable: false),
                    skin_test_id = table.Column<int>(type: "int", nullable: false),
                    result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skin_test_result", x => x.result_id);
                });

            migrationBuilder.CreateTable(
                name: "skin_therapist",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    specialization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    yoe = table.Column<int>(type: "int", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<double>(type: "float", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skin_therapist", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_skin_therapist_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skin_therapist_schedule",
                schema: "dbo",
                columns: table => new
                {
                    schedule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skin_therapist_id = table.Column<int>(type: "int", nullable: false),
                    work_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skin_therapist_schedule", x => x.schedule_id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_staff_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "timeslot",
                schema: "dbo",
                columns: table => new
                {
                    time_slot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeslot", x => x.time_slot_id);
                });

            migrationBuilder.CreateTable(
                name: "treatment",
                schema: "dbo",
                columns: table => new
                {
                    treatment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    treatment_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatment", x => x.treatment_id);
                });

            migrationBuilder.CreateTable(
                name: "treatment_result",
                schema: "dbo",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    treatment_notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    completed_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatment_result", x => x.result_id);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                schema: "dbo",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    blog_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_comment_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_blog_blog_id",
                        column: x => x.blog_id,
                        principalSchema: "dbo",
                        principalTable: "blog",
                        principalColumn: "blog_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consulting_form",
                schema: "dbo",
                columns: table => new
                {
                    consulting_form_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staff_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    guest_id = table.Column<int>(type: "int", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consulting_form", x => x.consulting_form_id);
                    table.ForeignKey(
                        name: "FK_consulting_form_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_consulting_form_guest_guest_id",
                        column: x => x.guest_id,
                        principalSchema: "dbo",
                        principalTable: "guest",
                        principalColumn: "guest_id");
                    table.ForeignKey(
                        name: "FK_consulting_form_staff_staff_id",
                        column: x => x.staff_id,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking_time_slot",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    time_slot_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_time_slot", x => x.id);
                    table.ForeignKey(
                        name: "FK_booking_time_slot_booking_booking_id",
                        column: x => x.booking_id,
                        principalSchema: "dbo",
                        principalTable: "booking",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_time_slot_timeslot_time_slot_id",
                        column: x => x.time_slot_id,
                        principalSchema: "dbo",
                        principalTable: "timeslot",
                        principalColumn: "time_slot_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Fullname_Length",
                schema: "dbo",
                table: "account_info",
                sql: "LEN([full_name]) >= 6");

            migrationBuilder.CreateIndex(
                name: "IX_blog_account_id",
                schema: "dbo",
                table: "blog",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_time_slot_booking_id",
                schema: "dbo",
                table: "booking_time_slot",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_time_slot_time_slot_id",
                schema: "dbo",
                table: "booking_time_slot",
                column: "time_slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_account_id",
                schema: "dbo",
                table: "comment",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_blog_id",
                schema: "dbo",
                table: "comment",
                column: "blog_id");

            migrationBuilder.CreateIndex(
                name: "IX_consulting_form_customer_id",
                schema: "dbo",
                table: "consulting_form",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_consulting_form_guest_id",
                schema: "dbo",
                table: "consulting_form",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_consulting_form_staff_id",
                schema: "dbo",
                table: "consulting_form",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_account_id",
                schema: "dbo",
                table: "notification",
                column: "account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_time_slot",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "comment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "consulting_form",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "feedback",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "feedback_reply",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "message",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "notification",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "service",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "settings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test_question",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test_result",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_therapist",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_therapist_schedule",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "treatment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "treatment_result",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "booking",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "timeslot",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "blog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "guest",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "staff",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_account_info",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Fullname_Length",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropColumn(
                name: "full_name",
                schema: "dbo",
                table: "account_info");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "dbo",
                table: "account");

            migrationBuilder.RenameColumn(
                name: "avatar",
                schema: "dbo",
                table: "account_info",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                schema: "dbo",
                table: "account",
                newName: "created_date");

            migrationBuilder.AddColumn<int>(
                name: "account_info_id",
                schema: "dbo",
                table: "account_info",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "background",
                schema: "dbo",
                table: "account_info",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                schema: "dbo",
                table: "account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                schema: "dbo",
                table: "account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "dbo",
                table: "account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login",
                schema: "dbo",
                table: "account",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_account_info",
                schema: "dbo",
                table: "account_info",
                column: "account_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_info_account_id",
                schema: "dbo",
                table: "account_info",
                column: "account_id",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Fullname_Length",
                schema: "dbo",
                table: "account",
                sql: "LEN([full_name]) >= 6");
        }
    }
}
