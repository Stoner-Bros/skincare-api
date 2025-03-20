using APP.API.Extensions;
using APP.API.Middlewares;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.DAL;
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

            builder.Services.AddControllers();

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

            app.UseAuthentication();
            // Custom middleware check token revoked
            app.UseTokenValidation();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
