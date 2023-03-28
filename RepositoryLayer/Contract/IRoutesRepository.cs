using DataAccessLayer.Entity;

namespace RepositoryLayer.Contract
{
    public interface IRoutesRepository : IRepositoryBase<TblRoutes>
    {
        Task<(List<TblFlights>, List<TblRoutes>)> GetFlightWithRoutes(int agencyId, DateTime startDate, DateTime endDate);
    }
}
