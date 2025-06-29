using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FurEverCarePlatform.API
{
    public static class DependencyInjection
    {
        public static void AddApiService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var secret = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("JWT SecretKey is not configured in appsettings.json.");
            }

            services
                .AddAuthentication(opts =>
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
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(secret)
                            ),
                        };
                    }
                );
            services.AddAuthorization();

            services.AddSwaggerGen(c =>
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
            services.AddCors(option =>
            {
                option.AddPolicy(
                    "all",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                );
            });
        }
    }
}
