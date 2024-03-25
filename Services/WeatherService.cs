using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Xml.Linq;
using WeatherMaker.Models.Responses;

namespace WeatherMaker.Services
{
    class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T?> GetWeatherData<T>(string lat, string lng)
        {
            string apiUrl = typeof(T) switch
            {
                Type t when t == typeof(CurrentWeatherResponse) => $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&current=temperature_2m,weather_code&forecast_days=1",
                Type t when t == typeof(DailyWeatherResponse) => $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&daily=weather_code,temperature_2m_max,temperature_2m_min",
                Type t when t == typeof(HourlyWeatherResponse) => $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&hourly=temperature_2m,weather_code&forecast_days=1",
                _ => throw new ArgumentException("Unsupported type", nameof(T))
            };

            return await GetAsync<T>(apiUrl).ConfigureAwait(false);
        }

        public async Task<T?> GetAllCityNames<T>()
        {
            var apiUrl = $"http://api.geonames.org/searchJSON?lang={CultureInfo.CurrentCulture.TwoLetterISOLanguageName}&featureClass=P&maxRows=1000&username=414105hc";
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

            return alternateNames.FirstOrDefault() ?? doc.Element("geoname")?.Element("name")?.Value ?? "Unknown";
        }

        private async Task<T?> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }

        private async Task<string> GetAsyncXml(string url)
        {
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
