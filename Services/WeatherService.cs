using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Net.Http.Json;
using WeatherMaker.Models.Responses;
using XPlat.Device.Geolocation;
using static System.Net.WebRequestMethods;
using System.Xml;
using System.Xml.Linq;

namespace WeatherMaker.Services
{
    class WeatherService : IWeatherService
    {
        public async Task<T?> GetWeatherData<T>(string lat, string lng)
        {
            string apiUrl = string.Empty;
            if (typeof(T) == typeof(CurrentWeatherResponse))
            {
                apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&current=temperature_2m,weather_code&forecast_days=1";
            }
            else if (typeof(T) == typeof(DailyWeatherResponse))
            {
                apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&daily=weather_code,temperature_2m_max,temperature_2m_min";
            }
            else if (typeof(T) == typeof(HourlyWeatherResponse))
            {
                apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&hourly=temperature_2m,weather_code&forecast_days=1";
            }
            else
            {
                throw new ArgumentException("Unsupported type", nameof(T));
            }

            return await GetAsync<T>(apiUrl).ConfigureAwait(false);
        }

        public async Task<T?> GetAllCityNames<T>()
        {
            string? apiUrl;
            if (AppSettings.Language == "English")
            {
                apiUrl = "http://api.geonames.org/searchJSON?lang=en&featureClass=P&maxRows=1000&username=414105hc";
            } else
            {
                apiUrl = "http://api.geonames.org/searchJSON?lang=ru&featureClass=P&maxRows=1000&username=414105hc";
            }
            

            return await GetAsync<T>(apiUrl).ConfigureAwait(false);
        }

        public async Task<string> GetCityName(string geonameId, string lang)
        {
            string apiUrl = $"http://api.geonames.org/get?geonameId={geonameId}&username=414105hc";
            var xmlString = await GetAsyncXml(apiUrl).ConfigureAwait(false);

            XDocument doc = XDocument.Parse(xmlString);

            var alternateNames = doc.Descendants("alternateName")
                .Where(a => a.Attribute("lang")?.Value == lang
                    && a.Attribute("isPreferredName")?.Value == "true")
                .Select(a => a.Value);

            if (alternateNames.Any())
                return alternateNames.First();

            // Если нет альтернативного имени для указанного языка,
            // возвращаем оригинальное имя города.
            return doc.Element("geoname")?.Element("name")?.Value ?? "Unknown";
        }

        private static async Task<T?> GetAsync<T>(string url)
        {
            using HttpClient client = new();
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseObject = await response.Content.ReadFromJsonAsync<T>();
            return responseObject;
        }

        public static async Task<string> GetAsyncXml(string url)
        {
            using HttpClient client = new();
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseObject = await response.Content.ReadAsStringAsync();
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
