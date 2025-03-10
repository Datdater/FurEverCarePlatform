using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FurEverCarePlatform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
