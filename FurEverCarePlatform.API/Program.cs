using System.Text;
using FurEverCarePlatform.API.Middleware;
using FurEverCarePlatform.Application;
using FurEverCarePlatform.Application.Features.Image;
using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence;
using FurEverCarePlatform.Persistence.DatabaseContext;
using FurEverCarePlatform.Persistence.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FurEverCarePlatform.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings")
            );

            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddPersistenceService();
            builder.Services.ApplicationService();
            builder.Services.AddApiService(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("all");
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Seed the database with initial data
            SeedDatabase(app);

            app.Run();
        }

        private static void SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PetDatabaseContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            try
            {
                SeedData.SeedAsync(context, userManager).Wait();
                Console.WriteLine("Database seeded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding database: {ex.Message}");
            }
        }
    }
}
