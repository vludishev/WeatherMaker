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
        public List<TemperatureUnitInfo> TemperatureUnits { get; set; }
        public TemperatureUnitInfo SelectedTempUnit { get; set; }
        public List<LanguageModel> Languages { get; set; }
        public LanguageModel SelectedLanguage { get; set; }
        public string Test { get; set; }

        private readonly IWeatherService WeatherService;

        public SettingsPageVM()
        {
            WeatherService = new WeatherService();
            Initialization();
        }

        private void Initialization()
        {
            Autorun = bool.TryParse(AppSettings.Autorun, out bool autorun) && autorun;
            Cities = AppSettings.GetCities();
            SelectedCity = new GeoInfo
            {
                Name = AppSettings.SelectedCity,
                Latitude = AppSettings.Latitude,
                Longitude = AppSettings.Longitude,
            };

            TemperatureUnits = new List<TemperatureUnitInfo>
            {
                new TemperatureUnitInfo { Value = TemperatureUnit.Celsius, LocalizedValue = GetLocalizedTemperatureUnit(TemperatureUnit.Celsius) },
                new TemperatureUnitInfo { Value = TemperatureUnit.Kelvin, LocalizedValue = GetLocalizedTemperatureUnit(TemperatureUnit.Kelvin) },
                new TemperatureUnitInfo { Value = TemperatureUnit.Fahrenheit, LocalizedValue = GetLocalizedTemperatureUnit(TemperatureUnit.Fahrenheit) }
            };

            SelectedTempUnit = TemperatureUnits.Find(unit => unit.Value.ToString() == AppSettings.TemperatureUnit);

            Languages = new List<LanguageModel>
            {
                new LanguageModel { Value = Language.Russian, LocalizedValue = GetLocalizedLanguage(Language.Russian) },
                new LanguageModel { Value = Language.English, LocalizedValue = GetLocalizedLanguage(Language.English) }
            };

            SelectedLanguage = Languages.Find(lang => lang.Value.ToString() == AppSettings.Language);
        }

        private string GetLocalizedTemperatureUnit(TemperatureUnit unit) => LocalizedLogic.Instance[unit.ToString()];

        private string GetLocalizedLanguage(string lang) => LocalizedLogic.Instance[lang];
    }
}
