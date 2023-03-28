using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class TblFlights : BaseModel
    {

        [ForeignKey(nameof(Route))]
        public int route_id { get; set; }
        public DateTime departure_time { get; set; }
        public DateTime arrival_time { get; set; }
        public int airline_id { get; set; }

        public virtual TblRoutes Route { get; set; }
    }

}
