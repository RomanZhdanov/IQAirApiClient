using System.Net;

namespace IQAirApiClient.Models
{
    public class Result<T> where T : class
    {
        public string Status { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        
        public T? Data { get; set; }
    }
}