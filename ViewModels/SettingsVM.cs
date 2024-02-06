using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMaker.Models;
using WeatherMaker.Models.Responses;
using WeatherMaker.Services;
using WPFLocalizeExtension.Engine;

namespace WeatherMaker.ViewModels
{
    public class SettingsVM
    {
        public bool Autorun { get; set; }
        public List<GeoInfo> Cities { get; set; }
        public GeoInfo SelectedCity { get; set; }

        public List<TemperatureUnitInfo> TemperatureUnits { get; set; } = new List<TemperatureUnitInfo>() 
        {
            new ()
            {
                Value = TemperatureUnit.Celsius,
                LocalizedValue = LocalizedLogic.Instance[TemperatureUnit.Celsius.ToString()]
            },
            new ()
            {
                Value = TemperatureUnit.Kelvin,
                LocalizedValue = LocalizedLogic.Instance[TemperatureUnit.Kelvin.ToString()]
            },
            new ()
            {
                Value = TemperatureUnit.Fahrenheit,
                LocalizedValue = LocalizedLogic.Instance[TemperatureUnit.Fahrenheit.ToString()]
            },
        };

        public TemperatureUnitInfo SelectedTempUnit { get; set; }

        public List<string> Languages { get; set; } = new List<string>() { "Русский", "English"};

        public string SelectedLanguage { get; set; }

        private IWeatherService _weatherService { get; set; }

        public SettingsVM()
        {
            _weatherService = new WeatherService();

            //LocalizedLogic.Instance["Celsius"], LocalizedLogic.Instance["Kelvin"], LocalizedLogic.Instance["Fahrenheit"]
            Autorun = bool.Parse(AppSettings.Autorun ?? "false");
            Cities = _weatherService.GetAllCityNames<GeolocationModel>().Result.Geonames;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            SelectedCity = new GeoInfo() { Name = AppSettings.SelectedCity, LocalizedValue = _weatherService.GetCityName(AppSettings.GeonameId, currentCulture.TwoLetterISOLanguageName).Result };
            var tempEnum = Enum.Parse<TemperatureUnit>(AppSettings.TemperatureUnit);
            SelectedTempUnit = new TemperatureUnitInfo() { Value = tempEnum, LocalizedValue = LocalizedLogic.Instance[tempEnum.ToString()] };
            SelectedLanguage = AppSettings.Language;
        }


    }
}
