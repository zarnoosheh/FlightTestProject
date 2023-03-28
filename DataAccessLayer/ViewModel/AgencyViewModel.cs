using DataAccessLayer.Entity;

namespace DataAccessLayer.ViewModel
{
    public class AgencyViewModel
    {
        public int AgencyId { get; set; }
        public List<TblSubscriptions> Subscriptions { get; set; }
    }
}
