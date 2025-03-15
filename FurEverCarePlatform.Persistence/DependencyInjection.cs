using FurEverCarePlatform.Persistence.Repositories;
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
            services.AddScoped<IPetServiceDetailRepository, PetServiceDetailRepository>();

            return services;
        }

        public static IServiceCollection AddIdentityService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<PetDatabaseContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("PetDB"))
            );
            // Use IdentityUser<Guid> and IdentityRole<Guid> to ensure both are consistent

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
