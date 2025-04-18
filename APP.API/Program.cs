﻿using APP.API.Extensions;
using APP.API.Hubs;
using APP.API.Middlewares;
using APP.BLL.BackgroundTask;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.DAL;
using APP.Entity.Entities;
using APP.Entity.Mapper;
using APP.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APP.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<AppDbContext>(
                opts => opts.UseSqlServer(builder.Configuration["ConnectionStrings:DBDefault"],
                options =>
            {
                options.MigrationsAssembly("APP.API");
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "app");
            }));

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "https://seoulspa.vercel.app") // Replace with your frontend URLs
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            //Add custom JWT Authentication
            builder.AddAppAuthetication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            //builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<JwtUtil>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<ITreatmentService, TreatmentService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<ISkinTherapistService, SkinTherapistService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
            builder.Services.AddScoped<ISkinTestService, SkinTestService>();
            builder.Services.AddScoped<ISkinTestQuestionService, SkinTestQuestionService>();
            builder.Services.AddScoped<ISkinTestAnswerService, SkinTestAnswerService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ISkinTestResultService, SkinTestResultService>();
            builder.Services.AddScoped<IConsultingFormService, ConsultingFormService>();
            builder.Services.AddScoped<ISkinTherapistScheduleService, SkinTherapistScheduleService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IFeedbackReplyService, FeedbackReplyService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IOverviewService, OverviewService>();

            builder.Services.AddScoped<MomoService>();
            // Đăng ký Background Service
            builder.Services.AddHostedService<BookingStatusUpdater>();

            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddControllers();
            builder.Services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Custom swagger
            builder.AddAppSwagger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend"); // Apply the CORS policy

            app.UseAuthentication();
            // Custom middleware check token revoked
            app.UseTokenValidation();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}
