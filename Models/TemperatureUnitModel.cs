using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMaker.Models
{
    public enum TemperatureUnit
    {
        Celsius,
        Kelvin,
        Fahrenheit
    }

    public class TemperatureUnitInfo
    {
        public TemperatureUnit Value { get; set; }
        public string LocalizedValue { get; set; }
    }
}
