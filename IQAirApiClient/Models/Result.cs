using System.Collections.Generic;

namespace IQAirApiClient.Models
{
    public class Result<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
}