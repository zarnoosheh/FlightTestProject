using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Contract;
using RepositoryLayer.Repositories;

namespace IocLayer
{
    public static class RepositoryInstance
    {
        public static void AddRepositoryInstance(this IServiceCollection services)
        {
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IRoutesRepository, RoutesRepository>();

        }
    }
}