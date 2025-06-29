using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using FurEverCarePlatform.Application.Features.Image;
using FurEverCarePlatform.Persistence.Repositories;
using FurEverCarePlatform.Persistence.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurEverCarePlatform.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPetServiceDetailRepository, PetServiceDetailRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProfileService, ProfileService>();
            return services;
        }

        public static IServiceCollection AddIdentityService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<PetDatabaseContext>(option =>
                option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );
            // Add Redis Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "CartInstance_";
            });

            // Register repositories
            services.AddScoped<ICartRepository, RedisCartRepository>();

            services
                .AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<PetDatabaseContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = false;
            });

            return services;
        }
    }
}
