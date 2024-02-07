using System;
using System.Collections.Specialized;
using System.Configuration;
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
        private static Configuration GetConfiguration()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

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

        public static string GeonameId
        {
            get { return GetValue("GeonameId"); }
            set { SetValue("GeonameId", value); }
        }

        public static string Latitude
        {
            get { return GetValue("Latitude"); }
            set { SetValue("Latitude", value); }
        }

        public static void SetDataCitiesSection(string jsonData)
        {
            var config = GetConfiguration();

            // Получаем пользовательскую секцию, проверяя, существует ли она
            CustomSection customSection = config.GetSection("сitiesSection") as CustomSection;

            // Если секции не существует, создаем новую
            customSection = new CustomSection();
            //customSection.CitiesJson = JsonSerializer.Serialize(new WeatherService().GetAllCityNames<GeolocationResponse>().Result.Geonames);
            config.Sections.Add("сitiesSection", customSection);

            // Сохраняем изменения в конфигурационном файле
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static List<GeoInfo> GetCities()
        {
            var config = GetConfiguration();

            CustomSection сitiesSection = config.GetSection("сitiesSection") as CustomSection;

            if (string.IsNullOrEmpty(сitiesSection?.CitiesJson))
            {
                var customSection = new CustomSection();
                config.Sections.Add("сitiesSection", customSection);
                var cities = new WeatherService().GetAllCityNames<GeolocationResponse>().Result.Geonames;
                customSection.CitiesJson = JsonSerializer.Serialize(cities);
                config.Save(ConfigurationSaveMode.Modified);
                return cities;
            }

            string citiesJson = сitiesSection.CitiesJson;
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
