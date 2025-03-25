using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.API.Migrations
{
    /// <inheritdoc />
    public partial class initAppDbV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.account_id);
                    table.CheckConstraint("CK_Email_Length", "LEN([email]) >= 6");
                    table.CheckConstraint("CK_Email_Valid", "CHARINDEX('@', [email]) > 0");
                    table.CheckConstraint("CK_Role_Valid", "[role] IN ('Customer', 'Skin Therapist', 'Staff', 'Manager')");
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
                name: "service",
                schema: "dbo",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    service_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    service_thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "account_info",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    other_info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_info", x => x.account_id);
                    table.CheckConstraint("CK_Fullname_Length", "LEN([full_name]) >= 6");
                    table.CheckConstraint("CK_Phone_Valid", "LEN([phone]) = 10");
                    table.ForeignKey(
                        name: "FK_account_info_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        principalColumn: "account_id");
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
                name: "expired_token",
                schema: "dbo",
                columns: table => new
                {
                    token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    invalidation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expired_token", x => x.token);
                    table.ForeignKey(
                        name: "FK_expired_token_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "refresh_token",
                schema: "dbo",
                columns: table => new
                {
                    refresh_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "FK_refresh_token_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "dbo",
                        principalTable: "account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "skin_therapist",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    specialization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "staff",
                schema: "dbo",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
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
                name: "treatment",
                schema: "dbo",
                columns: table => new
                {
                    treatment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    treatment_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    treatment_thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_treatment_service_service_id",
                        column: x => x.service_id,
                        principalSchema: "dbo",
                        principalTable: "service",
                        principalColumn: "service_id");
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
                    table.ForeignKey(
                        name: "FK_skin_test_question_skin_test_skin_test_id",
                        column: x => x.skin_test_id,
                        principalSchema: "dbo",
                        principalTable: "skin_test",
                        principalColumn: "skin_test_id");
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
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_comment_blog_blog_id",
                        column: x => x.blog_id,
                        principalSchema: "dbo",
                        principalTable: "blog",
                        principalColumn: "blog_id");
                });

            migrationBuilder.CreateTable(
                name: "skin_test_answer",
                schema: "dbo",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skin_test_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    guest_id = table.Column<int>(type: "int", nullable: true),
                    answers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skin_test_answer", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK_skin_test_answer_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_skin_test_answer_guest_guest_id",
                        column: x => x.guest_id,
                        principalSchema: "dbo",
                        principalTable: "guest",
                        principalColumn: "guest_id");
                    table.ForeignKey(
                        name: "FK_skin_test_answer_skin_test_skin_test_id",
                        column: x => x.skin_test_id,
                        principalSchema: "dbo",
                        principalTable: "skin_test",
                        principalColumn: "skin_test_id");
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
                    table.ForeignKey(
                        name: "FK_skin_test_result_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_skin_test_result_guest_guest_id",
                        column: x => x.guest_id,
                        principalSchema: "dbo",
                        principalTable: "guest",
                        principalColumn: "guest_id");
                    table.ForeignKey(
                        name: "FK_skin_test_result_skin_test_skin_test_id",
                        column: x => x.skin_test_id,
                        principalSchema: "dbo",
                        principalTable: "skin_test",
                        principalColumn: "skin_test_id");
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
                    table.ForeignKey(
                        name: "FK_skin_therapist_schedule_skin_therapist_skin_therapist_id",
                        column: x => x.skin_therapist_id,
                        principalSchema: "dbo",
                        principalTable: "skin_therapist",
                        principalColumn: "account_id");
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
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "booking",
                schema: "dbo",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    treatment_id = table.Column<int>(type: "int", nullable: false),
                    skin_therapist_id = table.Column<int>(type: "int", nullable: true),
                    staff_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    guest_id = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_booking_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "dbo",
                        principalTable: "customer",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_booking_guest_guest_id",
                        column: x => x.guest_id,
                        principalSchema: "dbo",
                        principalTable: "guest",
                        principalColumn: "guest_id");
                    table.ForeignKey(
                        name: "FK_booking_skin_therapist_skin_therapist_id",
                        column: x => x.skin_therapist_id,
                        principalSchema: "dbo",
                        principalTable: "skin_therapist",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_booking_staff_staff_id",
                        column: x => x.staff_id,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_booking_treatment_treatment_id",
                        column: x => x.treatment_id,
                        principalSchema: "dbo",
                        principalTable: "treatment",
                        principalColumn: "treatment_id");
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
                    date = table.Column<DateOnly>(type: "date", nullable: false),
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
                        principalColumn: "booking_id");
                    table.ForeignKey(
                        name: "FK_booking_time_slot_timeslot_time_slot_id",
                        column: x => x.time_slot_id,
                        principalSchema: "dbo",
                        principalTable: "timeslot",
                        principalColumn: "time_slot_id");
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
                    table.ForeignKey(
                        name: "FK_feedback_booking_booking_id",
                        column: x => x.booking_id,
                        principalSchema: "dbo",
                        principalTable: "booking",
                        principalColumn: "booking_id");
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
                    table.ForeignKey(
                        name: "FK_payment_booking_booking_id",
                        column: x => x.booking_id,
                        principalSchema: "dbo",
                        principalTable: "booking",
                        principalColumn: "booking_id");
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
                    table.ForeignKey(
                        name: "FK_treatment_result_booking_booking_id",
                        column: x => x.booking_id,
                        principalSchema: "dbo",
                        principalTable: "booking",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_feedback_reply_feedback_feedback_id",
                        column: x => x.feedback_id,
                        principalSchema: "dbo",
                        principalTable: "feedback",
                        principalColumn: "feedback_id");
                    table.ForeignKey(
                        name: "FK_feedback_reply_staff_staff_id",
                        column: x => x.staff_id,
                        principalSchema: "dbo",
                        principalTable: "staff",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_email",
                schema: "dbo",
                table: "account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_blog_account_id",
                schema: "dbo",
                table: "blog",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_customer_id",
                schema: "dbo",
                table: "booking",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_guest_id",
                schema: "dbo",
                table: "booking",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_skin_therapist_id",
                schema: "dbo",
                table: "booking",
                column: "skin_therapist_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_staff_id",
                schema: "dbo",
                table: "booking",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_treatment_id",
                schema: "dbo",
                table: "booking",
                column: "treatment_id");

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
                name: "IX_expired_token_account_id",
                schema: "dbo",
                table: "expired_token",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_booking_id",
                schema: "dbo",
                table: "feedback",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_feedback_reply_feedback_id",
                schema: "dbo",
                table: "feedback_reply",
                column: "feedback_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_reply_staff_id",
                schema: "dbo",
                table: "feedback_reply",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_account_id",
                schema: "dbo",
                table: "notification",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_booking_id",
                schema: "dbo",
                table: "payment",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_account_id",
                schema: "dbo",
                table: "refresh_token",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_answer_customer_id",
                schema: "dbo",
                table: "skin_test_answer",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_answer_guest_id",
                schema: "dbo",
                table: "skin_test_answer",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_answer_skin_test_id",
                schema: "dbo",
                table: "skin_test_answer",
                column: "skin_test_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_question_skin_test_id",
                schema: "dbo",
                table: "skin_test_question",
                column: "skin_test_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_result_customer_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_result_guest_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "guest_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_test_result_skin_test_id",
                schema: "dbo",
                table: "skin_test_result",
                column: "skin_test_id");

            migrationBuilder.CreateIndex(
                name: "IX_skin_therapist_schedule_skin_therapist_id",
                schema: "dbo",
                table: "skin_therapist_schedule",
                column: "skin_therapist_id");

            migrationBuilder.CreateIndex(
                name: "IX_treatment_service_id",
                schema: "dbo",
                table: "treatment",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_treatment_result_booking_id",
                schema: "dbo",
                table: "treatment_result",
                column: "booking_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_info",
                schema: "dbo");

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
                name: "expired_token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "feedback_reply",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "notification",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "refresh_token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "settings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test_answer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test_question",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test_result",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_therapist_schedule",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "treatment_result",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "timeslot",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "blog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "feedback",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_test",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "booking",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "guest",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "skin_therapist",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "staff",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "treatment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "account",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "service",
                schema: "dbo");
        }
    }
}
