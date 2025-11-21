using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PodcastAPI.Domain.Interfaces;
using PodcastAPI.Persistence.Contexts;
using PodcastAPI.Persistence.Repositories;

namespace PodcastAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PodcastAPIDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("PodcastAPIConnectionString")));
            services.AddScoped<IPodcastRepository, PodcastRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            return services;
        }
    }
}
