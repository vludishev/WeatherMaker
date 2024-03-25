using System.Text.Json.Serialization;

namespace WeatherMaker.Models.Responses
{
    public class CurrentWeather
    {
        [JsonPropertyName("Temperature_2m")]
        public double Temperature { get; set; }
        [JsonPropertyName("Weather_code")]
        public int WeatherCode { get; set; }
    }

    public class CurrentWeatherResponse
    {
        public CurrentWeather? Current { get; set; }
    }
}
