using Microsoft.Extensions.DependencyInjection;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Infrastructure.Services;

namespace PodcastAPI.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRssService, RssService>();

            return services;
        }
    }
}
