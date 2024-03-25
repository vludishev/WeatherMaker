using System.Text.Json.Serialization;

namespace WeatherMaker.Models.Responses
{
    public class HourlyWeather
    {
        [JsonPropertyName("Time")]
        public List<string>? Date { get; set; }
        [JsonPropertyName("Temperature_2m")]
        public List<double>? Temperature { get; set; }
        [JsonPropertyName("Weather_code")]
        public List<int>? WeatherCode { get; set; }
    }

    public class HourlyWeatherResponse
    {
        public HourlyWeather? Hourly { get; set; }
    }
}
