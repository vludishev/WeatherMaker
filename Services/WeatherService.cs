using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using WeatherMaker.Models;

namespace WeatherMaker.Services
{
    class WeatherService : IWeatherService
    {
        public async Task<T?> GetWeatherData<T>()
        {
            string apiUrl = string.Empty;
            if (typeof(T) == typeof(CurrentWeatherDataModel))
            {
                apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude=55.0415&longitude=82.9346&current=temperature_2m,weather_code&forecast_days=1";
            }
            else if (typeof(T) == typeof(WeatherDataDailyModel))
            {
                apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude=55.0415&longitude=82.9346&daily=weather_code,temperature_2m_max,temperature_2m_min";
            }
            else if (typeof(T) == typeof(HourlyWeatherResponse))
            {
                apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude=55.0415&longitude=82.9346&hourly=temperature_2m,weather_code&forecast_days=1";
            }
            else
            {
                throw new ArgumentException("Unsupported type", nameof(T));
            }

            return await GetAsync<T>(apiUrl).ConfigureAwait(false);
        }

        public async Task<string> GetCurrentCity(double latitude, double longitude)
        {
            var apiKey = "AIzaSyACHApcgi8nMRxar5fgWKbZWVwvaAIus0g";
            string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";
            var result = await GetAsync(apiUrl).ConfigureAwait(false);

            return (string)result["results"][0]["address_components"]
                                    .FirstOrDefault(ac => ac["types"].Any(t => t.ToString() == "locality"))?["long_name"];
        }

        private static async Task<T?> GetAsync<T>(string url)
        {
            using HttpClient client = new();
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseObject = await response.Content.ReadFromJsonAsync<T>();
            return responseObject;
        }

        private static async Task<JObject> GetAsync(string url)
        {
            using HttpClient client = new();
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            return responseObject;
        }
    }
}
