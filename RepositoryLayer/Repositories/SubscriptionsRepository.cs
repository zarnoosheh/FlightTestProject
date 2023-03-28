using DataAccessLayer.Entity;
using RepositoryLayer.Contract;

namespace RepositoryLayer.Repositories
{
    public class SubscriptionsRepository : RepositoryBase<TblSubscriptions>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(ApplicationContext Context) : base(Context) { }
    }
}
