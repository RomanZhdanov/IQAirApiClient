using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using IQAirApiClient.Models;

namespace IQAirApiClient
{
    public interface IQAirApi
    {
        Task<IList<CountryItem>?> ListSupportedCountries();
        
        Task<IList<StateItem>?> ListSupportedStatesInCountry(string country);
        
        Task<IList<CityItem>?> ListSupportedCitiesInState(string country, string state);

        Task<CityData> GetSpecifiedCityData(string city, string state, string country);
    }

    public class IQAirApiClient : IQAirApi
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public IQAirApiClient(string apiKey, HttpClient httpClient)
        {
            _apiKey = apiKey;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://api.airvisual.com/v2/");
        }

        public async Task<IList<CountryItem>?> ListSupportedCountries()
        {
            var query = HttpUtility.ParseQueryString("");
            query["key"] = _apiKey;
            
            var result = await _httpClient.GetFromJsonAsync<Result<IList<CountryItem>>>($"countries?{query}");

            return result?.Data;
        }

        public async Task<IList<StateItem>?> ListSupportedStatesInCountry(string country)
        {
            var query = HttpUtility.ParseQueryString("");
            query["key"] = _apiKey;
            query["country"] = country;
            
            var result = await _httpClient.GetFromJsonAsync<Result<IList<StateItem>>>($"states?{query}");

            return result?.Data;
        }
        
        public async Task<IList<CityItem>?> ListSupportedCitiesInState(string country, string state)
        {
            var query = HttpUtility.ParseQueryString("");
            query["key"] = _apiKey;
            query["country"] = country;
            query["state"] = state;
            
            var result = await _httpClient.GetFromJsonAsync<Result<IList<CityItem>>>($"cities?{query}");

            return result?.Data;
        }

        public async Task<CityData> GetSpecifiedCityData(string city, string state, string country)
        {
            var query = HttpUtility.ParseQueryString("");
            query["key"] = _apiKey;
            query["city"] = city;
            query["state"] = state;
            query["country"] = country;
            
            var result = await _httpClient.GetFromJsonAsync<Result<CityData>>($"city?{query}");

            return result?.Data;
        }
    }
}