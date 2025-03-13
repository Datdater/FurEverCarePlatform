
using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence;
using FurEverCarePlatform.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using FurEverCarePlatform.Application;
using FurEverCarePlatform.Persistence.Repositories;
using FurEverCarePlatform.API.Middleware;
namespace FurEverCarePlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddPersistenceService();

            builder.Services.ApplicationService();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("all", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            });

            builder.Services.AddSingleton<IEmailSender<AppUser>, NoOpEmailSender<AppUser>>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("all");
            app.MapIdentityApi<AppUser>();
			app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
