using WeatherMaker.Models;
using WeatherMaker.Models.Responses;

namespace WeatherMaker
{
    public static class Helper
    {

        public static double CalculateTemperature(double temperature, TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    return temperature;
                case TemperatureUnit.Kelvin:
                    return ConvertCelsiusToKelvin(temperature);
                case TemperatureUnit.Fahrenheit:
                    return ConvertCelsiusToFahrenheit(temperature);
                default:
                    break;
            }

            return 0;
        }

        private static double ConvertCelsiusToFahrenheit(double temperature)
        {
            return temperature * (9 / 5) + 32;
        }

        private static double ConvertCelsiusToKelvin(double temperature)
        {
            return temperature + 273.15;
        }
    }
}
