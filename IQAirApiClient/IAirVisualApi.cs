using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using IQAirApiClient.Models;
using Microsoft.Extensions.Configuration;

namespace IQAirApiClient
{
    public interface IAirVisualApi
    {
        Task<Result<IList<CountryItem>>> ListSupportedCountries();
        
        Task<Result<IList<StateItem>>> ListSupportedStatesInCountry(string country);
        
        Task<Result<IList<CityItem>>> ListSupportedCitiesInState(string country, string state);

        Task<Result<CityData>> GetSpecifiedCityData(string country, string state, string city);

        Task<Result<CityData>> GetNearestCityData(double lat, double lon);
    }

    public class AirVisualApiClient : IAirVisualApi
    {
        private readonly string? _apiKey;
        private readonly HttpClient _httpClient;

        public AirVisualApiClient(IConfiguration configuration, HttpClient httpClient)
        {
            _apiKey = configuration["IQAirKey"]; 
            if (_apiKey is null) throw new AggregateException("IQAir ApiKey is not configured");
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://api.airvisual.com/v2/");
        }

        public async Task<Result<IList<CountryItem>>> ListSupportedCountries()
        {
            return await GetRequest<IList<CountryItem>>("countries");
        }

        public async Task<Result<IList<StateItem>>> ListSupportedStatesInCountry(string country)
        {
            var query = HttpUtility.ParseQueryString("");
            query["country"] = country;
            
            return await GetRequest<IList<StateItem>>("states", query);
        }
        
        public async Task<Result<IList<CityItem>>> ListSupportedCitiesInState(string country, string state)
        {
            var query = HttpUtility.ParseQueryString("");
            query["country"] = country;
            query["state"] = state;
            
            return await GetRequest<IList<CityItem>>("cities", query);
        }

        public async Task<Result<CityData>> GetSpecifiedCityData(string country, string state, string city)
        {
            var query = HttpUtility.ParseQueryString("");
            query["country"] = country;
            query["state"] = state;
            query["city"] = city;
            
            return await GetRequest<CityData>("city", query);
        }

        public async Task<Result<CityData>> GetNearestCityData(double lat, double lon)
        {
            var query = HttpUtility.ParseQueryString("");
            query["lat"] = lat.ToString(CultureInfo.InvariantCulture);
            query["lon"] = lon.ToString(CultureInfo.InvariantCulture);
            
            return await GetRequest<CityData>("nearest_city", query);
        }

        private async Task<Result<T>> GetRequest<T>(string endpoint, NameValueCollection? query = null)
            where T : class
        {
            query ??= HttpUtility.ParseQueryString("");
            
            query["key"] = _apiKey;
            
            var response = await _httpClient.GetAsync($"{endpoint}?{query}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Result<T>>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                
                result ??= new Result<T>()
                {
                    Status = "unsuccessful",
                    Data = null
                };
                
                result.StatusCode = response.StatusCode;
                
                return result;
            }
            
            return new Result<T>
            {
                Status = "unsuccessful",
                StatusCode = response.StatusCode,
                Data = null
            };
        }
    }
}