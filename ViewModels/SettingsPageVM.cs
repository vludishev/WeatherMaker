using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using WeatherMaker.Models;
using WeatherMaker.Models.Responses;
using WeatherMaker.Services;

namespace WeatherMaker.ViewModels
{
    public class SettingsPageVM
    {
        public bool Autorun { get; set; }
        public List<GeoInfo> Cities { get; set; }
        public GeoInfo SelectedCity { get; set; }

        public List<TemperatureUnitInfo> TemperatureUnits { get; set; } =
        [
            new()
            {
                Value = TemperatureUnit.Celsius,
                LocalizedValue = LocalizedLogic.Instance[TemperatureUnit.Celsius.ToString()]
            },
            new()
            {
                Value = TemperatureUnit.Kelvin,
                LocalizedValue = LocalizedLogic.Instance[TemperatureUnit.Kelvin.ToString()]
            },
            new()
            {
                Value = TemperatureUnit.Fahrenheit,
                LocalizedValue = LocalizedLogic.Instance[TemperatureUnit.Fahrenheit.ToString()]
            },
        ];

        public TemperatureUnitInfo SelectedTempUnit { get; set; }

        public List<LanguageModel> Languages { get; set; } =
        [
            new() { Value = Language.Russian, LocalizedValue = LocalizedLogic.Instance[Language.Russian] },
            new() { Value = Language.English, LocalizedValue = LocalizedLogic.Instance[Language.English] }
        ];   

        public LanguageModel SelectedLanguage { get; set; }
        public string Test { get; set; }

        private IWeatherService WeatherService { get; set; }

        public SettingsPageVM()
        {
            WeatherService = new WeatherService();

            Initialization();
        }

        public void Initialization()
        {
            Autorun = bool.Parse(AppSettings.Autorun ?? "false");
            Cities = AppSettings.GetCities();   
            SelectedCity = new GeoInfo()
            {
                Name = WeatherService.GetCityName(AppSettings.GeonameId, CultureInfo.CurrentCulture.TwoLetterISOLanguageName).Result,
                Latitude = AppSettings.Latitude,
                Longitude = AppSettings.Longitude,
            };

            var tempEnum = Enum.Parse<TemperatureUnit>(AppSettings.TemperatureUnit);
            SelectedTempUnit = new TemperatureUnitInfo() { Value = tempEnum, LocalizedValue = LocalizedLogic.Instance[tempEnum.ToString()] };
            SelectedLanguage = new LanguageModel() { Value = AppSettings.Language, LocalizedValue = LocalizedLogic.Instance[AppSettings.Language] };
        }
    }
}
