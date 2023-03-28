using DataAccessLayer.Entity;

namespace DataAccessLayer.ViewModel
{
    public class ResultFlightViewModel
    {
        public int flight_id { get; set; }
        public int origin_city_id { get; set; }
        public int destination_city_id { get; set; }
        public DateTime departure_time { get; set; }
        public DateTime arrival_time { get; set; }
        public int airline_id { get; set; }
        public string status { get; set; }

        public ResultFlightViewModel(TblFlights Flight, string Status)
        {
            flight_id = Flight.Id;
            origin_city_id = Flight.Route.origin_city_id;
            destination_city_id = Flight.Route.destination_city_id;
            departure_time = Flight.departure_time;
            arrival_time = Flight.arrival_time;
            airline_id = Flight.airline_id;
            status = Status;
        }
    }
}
