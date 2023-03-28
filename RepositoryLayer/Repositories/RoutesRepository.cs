using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contract;
using System.Diagnostics;

namespace RepositoryLayer.Repositories
{
    public class RoutesRepository : RepositoryBase<TblRoutes>, IRoutesRepository
    {
        private readonly ApplicationContext _context;

        public RoutesRepository(ApplicationContext Context) : base(Context)
        {
            _context = Context;


        }
        public async Task<(List<TblFlights>, List<TblRoutes>)> GetFlightWithRoutes(int agencyId, DateTime startDate, DateTime endDate)
        {
            var stopwatch = Stopwatch.StartNew();
            var flightWithRoutesAndSubscriptions = await _context.Set<TblFlights>()
                .Join(_context.Set<TblRoutes>(),
                    flight => flight.route_id,
                    route => route.Id,
                    (flight, route) => new { Flight = flight, Route = route })
                .Join(_context.Set<TblSubscriptions>(),
                    fr => new { fr.Route.origin_city_id, fr.Route.destination_city_id },
                    subscription => new { subscription.origin_city_id, subscription.destination_city_id },
                    (fr, subscription) => new { fr.Flight, fr.Route, Subscription = subscription })
                .Where(x => x.Subscription.agency_id == agencyId &&
                            x.Flight.departure_time >= startDate &&
                            x.Flight.departure_time <= endDate)
                .ToListAsync();
            Console.WriteLine($"Fetch From Db: {stopwatch.Elapsed.TotalMilliseconds} ms");
            stopwatch.Stop();

            List<TblFlights> flights = flightWithRoutesAndSubscriptions.Select(x => x.Flight).ToList();
            List<TblRoutes> Routes = flightWithRoutesAndSubscriptions.Select(x => x.Route).ToList();
            return (flights, Routes);
        }


    }
}
