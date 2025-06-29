using System.Reflection;
using System.Text;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FurEverCarePlatform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ICurrentTime, CurrentTimeService>();
            //services.AddScoped<AuthService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            // Add Identity
            //services.AddIdentity<AppUser, IdentityRole>(options =>
            //    {
            //        // Password settings
            //        options.Password.RequireDigit = true;
            //        options.Password.RequireLowercase = true;
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequireNonAlphanumeric = true;
            //        options.Password.RequiredLength = 8;

            //        // Lockout settings
            //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            //        options.Lockout.MaxFailedAccessAttempts = 5;

            //        // User settings
            //        options.User.RequireUniqueEmail = true;
            //    })
            //    .AddEntityFrameworkStores<IdentityContext>()
            //    .AddDefaultTokenProviders();

            // Register Redis token service
            services.AddScoped<RedisTokenService>();
            services.AddScoped<EmailService>();
            // Register user service
            services.AddScoped<UserService>();
            // Add services
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<AuthService>();

            return services;
        }
    }
}
