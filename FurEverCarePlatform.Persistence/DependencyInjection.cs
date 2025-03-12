using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurEverCarePlatform.Persistence
{
    public static class DependencyInjection
    {

		public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
		{
			//services.AddDbContext<PetDatabaseContext>(option => option.UseSqlServer(configuration.GetConnectionString("PetDatabase")));
			// Use IdentityUser<Guid> and IdentityRole<Guid> to ensure both are consistent

			services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<PetDatabaseContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// User settings
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
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
			});

			return services;
		}
	}
}
