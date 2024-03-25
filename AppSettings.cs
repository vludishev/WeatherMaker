using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WeatherMaker.Models.Responses;
using WeatherMaker.Services;

namespace WeatherMaker
{
    // Класс для определения пользовательской секции
    public class CustomSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(CitiesJson))]
        public string CitiesJson
        {
            get { return (string)this[nameof(CitiesJson)]; }
            set { this[nameof(CitiesJson)] = value; }
        }
    }

    public static class AppSettings
    {
        private static Configuration GetConfiguration() => ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        private static string GetValue(string key) => ConfigurationManager.AppSettings[key];

        private static void SetValue(string key, string value)
        {
            Configuration config = GetConfiguration();

            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string SelectedCity
        {
            get { return GetValue("SelectedCity"); }
            set { SetValue("SelectedCity", value); }
        }

        public static string Latitude
        {
            get { return GetValue("Latitude"); }
            set { SetValue("Latitude", value); }
        }

        public static void SetDataCitiesSection(IEnumerable<GeoInfo> cities)
        {
            var config = GetConfiguration();
            CustomSection customSection = config.GetSection("сitiesSection") as CustomSection ?? new CustomSection();
            customSection.CitiesJson = JsonSerializer.Serialize(cities);

            // Удаляем существующую секцию, если она есть, перед добавлением новой
            if (config.Sections["сitiesSection"] != null)
                config.Sections.Remove("сitiesSection");

            config.Sections.Add("сitiesSection", customSection);
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static List<GeoInfo> GetCities()
        {
            var config = GetConfiguration();
            CustomSection citiesSection = config.GetSection("сitiesSection") as CustomSection;
            string citiesJson = citiesSection?.CitiesJson;

            if (string.IsNullOrEmpty(citiesJson))
            {
                var cities = new WeatherService().GetAllCityNames<GeolocationResponse>().Result.Geonames.ToList();
                SetDataCitiesSection(cities);
                return cities;
            }

            return JsonSerializer.Deserialize<List<GeoInfo>>(citiesJson);
        }

        public static string Longitude
        {
            get { return GetValue("Longitude"); }
            set { SetValue("Longitude", value); }
        }

        public static string TemperatureUnit
        {
            get { return GetValue("TemperatureUnit"); }
            set { SetValue("TemperatureUnit", value); }
        }

        public static string Language
        {
            get { return GetValue("Language"); }
            set { SetValue("Language", value); }
        }

        public static string Autorun
        {
            get { return GetValue("Autorun"); }
            set { SetValue("Autorun", value); }
        }
    }
}
