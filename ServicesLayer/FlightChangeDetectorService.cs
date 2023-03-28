using DataAccessLayer.Entity;
using ServicesLayer.Contract;

namespace ServicesLayer
{
    public class FlightChangeDetectorService : IFlightChangeDetectorService
    {
        private readonly IRoutesService _routesService;
        private readonly int TotalActiveThread = 2;
        public FlightChangeDetectorService(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<(List<TblFlights> newFlights, List<TblFlights> discontinuedFlights)> DetectionAlgorithm(List<TblFlights> flights, List<TblRoutes> routes)
        {
            var newFlights = new List<TblFlights>();
            var discontinuedFlights = new List<TblFlights>();
            var tolerance = TimeSpan.FromMinutes(30);
            var flightsByAirlineAndDeparture = flights
                .GroupBy(f => new { f.airline_id, f.departure_time.TimeOfDay })
                .ToDictionary(g => g.Key, g => g.ToList());
            foreach (var flight in flights)
            {
                var previousWeekDeparture = flight.departure_time.AddDays(-7);
                var nextWeekDeparture = flight.departure_time.AddDays(7);
                if (flightsByAirlineAndDeparture.TryGetValue(new { flight.airline_id, flight.departure_time.TimeOfDay }, out var sameTimeFlights))
                {
                    var isNewFlight = !sameTimeFlights.Any(sameTimeFlight => Math.Abs((sameTimeFlight.departure_time - previousWeekDeparture).TotalMinutes) <= tolerance.TotalMinutes);
                    var isDiscontinuedFlight = !sameTimeFlights.Any(sameTimeFlight => Math.Abs((sameTimeFlight.departure_time - nextWeekDeparture).TotalMinutes) <= tolerance.TotalMinutes);
                    if (isNewFlight)
                    {
                        newFlights.Add(flight);
                    }
                    if (isDiscontinuedFlight)
                    {
                        discontinuedFlights.Add(flight);
                    }
                }
                else
                {
                    newFlights.Add(flight);
                    discontinuedFlights.Add(flight);
                }
            }
            return (newFlights, discontinuedFlights);
        }

        public async Task<(List<TblFlights>, List<TblRoutes>)> Filter(int agencyId, DateTime startDate, DateTime endDate)
        {
            var (Flights, Routes) = (await _routesService.GetFlightWithRoutes(agencyId, startDate, endDate));
            return (Flights, Routes);
        }


    }
}
