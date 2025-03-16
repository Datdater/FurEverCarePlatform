using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;


namespace FurEverCarePlatform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            //services.AddHttpContextAccessor();
            //services.AddScoped<IClaimService, ClaimService>();
            //services.AddScoped<ICurrentTime, CurrentTimeService>();
            services.AddScoped<AuthService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
        }
    }
}
