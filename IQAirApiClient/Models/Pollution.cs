using System;

namespace IQAirApiClient.Models
{
    public class Pollution
    {
        public DateTime Ts { get; set; }

        public int Aqius { get; set; }

        public string Mainus { get; set; }

        public int Aqicn { get; set; }

        public string Maincn { get; set; }
    }
}