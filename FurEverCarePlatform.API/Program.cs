using System.Text;
using FurEverCarePlatform.API.Middleware;
using FurEverCarePlatform.Application;
using FurEverCarePlatform.Application.Features.Image;
using FurEverCarePlatform.Persistence;
using FurEverCarePlatform.Persistence.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var secret = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("JWT SecretKey is not configured in appsettings.json.");
            }

            var key = Encoding.ASCII.GetBytes(secret);
            builder
                .Services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                    JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = false,
                            ValidateIssuerSigningKey = false,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(secret)
                            ),
                        };
                    }
                );
            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "FurEverCarePlatform API",
                        Description = "API for FurEverCarePlatform with JWT Authentication",
                    }
                );

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token in this field",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                };

                c.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                };

                c.AddSecurityRequirement(securityRequirement);
            });
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(
                    "all",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                );
            });
            //builder.Services.AddSingleton<IEmailSender<AppUser>, NoOpEmailSender<AppUser>>();

            var app = builder.Build();
            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            //    string[] roles = new[] { "Pet Owner", "Admin", "Employees", "Store Manager", "Store owner" };

            //    foreach (var role in roles)
            //    {

            //            var roleResult =  roleManager.CreateAsync(new IdentityRole<Guid>
            //            {
            //                Id = Guid.NewGuid(),
            //                Name = role,
            //                NormalizedName = role.ToUpper()
            //            });

            //    }
            //}
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("all");
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
