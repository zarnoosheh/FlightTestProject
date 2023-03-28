namespace DataAccessLayer.Entity
{
    public class TblSubscriptions : BaseModel
    {
        public int agency_id { get; set; }
        public int origin_city_id { get; set; }
        public int destination_city_id { get; set; }
    }
}
