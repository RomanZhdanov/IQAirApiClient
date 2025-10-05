namespace IQAirApiClient.Models
{
    public class CityData
    {
        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
        

        public Location Location { get; set; }

        public Forecast Current { get; set; }
    }
}