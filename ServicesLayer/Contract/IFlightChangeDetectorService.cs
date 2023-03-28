using DataAccessLayer.Entity;

namespace ServicesLayer.Contract
{
    public interface IFlightChangeDetectorService
    {

        Task<(List<TblFlights> newFlights, List<TblFlights> discontinuedFlights)> DetectionAlgorithm(List<TblFlights> flights, List<TblRoutes> routes);

        Task<(List<TblFlights>, List<TblRoutes>)> Filter(int agencyId, DateTime startDate, DateTime endDate);
    }
}
