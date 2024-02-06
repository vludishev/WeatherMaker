using System.Text.Json.Serialization;

namespace WeatherMaker.Models.Responses
{
    public class WeatherBase
    {
        [JsonPropertyName("Time")]
        public List<string>? Date { get; set; }

        [JsonPropertyName("Temperature_2m")]
        public List<double>? Temperature { get; set; }

        [JsonPropertyName("Weather_code")]
        public List<int>? WeatherCode { get; set; }
    }

    public class HourlyWeather : WeatherBase
    {
        // Дополнительные свойства для HourlyWeather, если необходимо
    }

    public class HourlyWeatherResponse
    {
        public HourlyWeather? Hourly { get; set; }
    }

    public class DailyWeather : WeatherBase
    {
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

    public class CurrentWeather : WeatherBase
    {
        // Дополнительные свойства для CurrentWeather, если необходимо
    }

    public class CurrentWeatherResponse
    {
        public CurrentWeather? Current { get; set; }
    }
}
