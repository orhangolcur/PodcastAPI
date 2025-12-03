using Microsoft.Extensions.DependencyInjection;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.Services;
using System.Reflection;

namespace PodcastAPI.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
