using System.Text.Json.Serialization;

namespace WeatherMaker.Models.Responses
{
    public class DailyWeather
    {
        [JsonPropertyName("Time")]
        public List<string>? Date { get; set; }
        [JsonPropertyName("Weather_code")]
        public List<int>? WeatherCode { get; set; }
        [JsonPropertyName("Temperature_2m_max")]
        public List<double>? MaxTemperature { get; set; }
        [JsonPropertyName("Temperature_2m_min")]
        public List<double>? MinTemperature { get; set; }
        public List<double>? AverageTemperatures
        {
            get
            {
                if (MaxTemperature == null || MinTemperature == null || MaxTemperature.Count != MinTemperature.Count)
                    return null;

                return MaxTemperature.Zip(MinTemperature, (max, min) => (max + min) / 2.0).ToList();
            }
        }
    }

    public class DailyWeatherResponse
    {
        public DailyWeather? Daily { get; set; }
    }
}