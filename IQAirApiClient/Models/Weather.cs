using System;

namespace IQAirApiClient.Models
{
    public class Weather
    {
        public DateTime Ts { get; set; }
        
        public int Tp { get; set; }
        
        public int Pr { get; set; }
        
        public int Hu { get; set; }
        
        public float Ws { get; set; }
        
        public int Wd { get; set; }
        
        public string Ic { get; set; }
    }
}