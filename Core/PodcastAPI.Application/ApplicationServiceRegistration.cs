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
            services.AddScoped<IPodcastService, PodcastService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
