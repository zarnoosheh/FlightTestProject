namespace DataAccessLayer.Entity
{
    public class TblRoutes : BaseModel
    {
        public int origin_city_id { get; set; }
        public int destination_city_id { get; set; }
        public DateTime departure_date { get; set; }

        #region ICollection
        public virtual ICollection<TblFlights> Flights { get; set; }

        #endregion
    }
}
