namespace WeatherMaker.Models
{
    public enum TemperatureUnit
    {
        Celsius,
        Kelvin,
        Fahrenheit
    }

    public class TemperatureUnitInfo : LocalizationExt
    {
        public new TemperatureUnit Value
        {
            get { return Enum.TryParse<TemperatureUnit>(LocalizedValue, out var res) 
                    ? res : TemperatureUnit.Celsius; }
            set { LocalizedValue = value.ToString(); }
        }
    }
}
