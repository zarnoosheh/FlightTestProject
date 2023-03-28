using Microsoft.Extensions.DependencyInjection;
using ServicesLayer;
using ServicesLayer.Contract;
using ServicesLayer.Services;

namespace IocLayer
{
    public static class ServicesDependency
    {
        public static void AddServicesDependency(this IServiceCollection services)
        {
            services.AddTransient<IFlightChangeDetectorService, FlightChangeDetectorService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<ISubscriptionsService, SubscriptionsService>();
            services.AddTransient<IRoutesService, RoutesService>();

        }
    }
}
