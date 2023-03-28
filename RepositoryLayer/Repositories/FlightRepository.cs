using DataAccessLayer.Entity;
using RepositoryLayer.Contract;

namespace RepositoryLayer.Repositories
{
    public class FlightRepository : RepositoryBase<TblFlights>, IFlightRepository
    {
        public FlightRepository(ApplicationContext Context) : base(Context) { }
    }
}
