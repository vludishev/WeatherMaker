using System.Configuration;

namespace WeatherMaker
{
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
