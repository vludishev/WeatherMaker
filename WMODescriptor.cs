using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WeatherMaker
{
    public enum WeatherCode
    {
        ClearSky = 0,
        MainlyClear = 1,
        PartlyCloudy = 2,
        Overcast = 3,
        Fog = 45,
        RimeFog = 48,
        DrizzleLight = 51,
        DrizzleModerate = 53,
        DrizzleDense = 55,
        FreezingDrizzleLight = 56,
        FreezingDrizzleDense = 57,
        RainSlight = 61,
        RainModerate = 63,
        RainHeavy = 65,
        FreezingRainLight = 66,
        FreezingRainHeavy = 67,
        SnowFallSlight = 71,
        SnowFallModerate = 73,
        SnowFallHeavy = 75,
        SnowGrains = 77,
        RainShowersSlight = 80,
        RainShowersModerate = 81,
        RainShowersViolent = 82,
        SnowShowersSlight = 85,
        SnowShowersHeavy = 86,
        ThunderstormSlight = 95,
        ThunderstormWithSlightHail = 96,
        ThunderstormWithHeavyHail = 99
    }

    public class WeatherCodeModel
    {
        public string Description { get; set; }
        public ImageSource WeatherImg { get; set; }
    }

    public static class WeatherCodeDescriptions
    {
        public static ImageSource GetWeatherImg(string fileName)
        {
            // Инициализация изображения
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = System.IO.Path.Combine(baseDirectory, $"Resources/Images/{fileName}");
            return new BitmapImage(new Uri(relativePath, UriKind.RelativeOrAbsolute));
        }

        public static WeatherCodeModel GetDescription(WeatherCode code)
        {
            return code switch
            {
                WeatherCode.ClearSky => new WeatherCodeModel() { Description = "Clear sky", WeatherImg = GetWeatherImg("ClearSky.png") },
                WeatherCode.MainlyClear => new WeatherCodeModel() { Description = "Mainly clear", WeatherImg = GetWeatherImg("MainlyClear.png") },
                WeatherCode.PartlyCloudy => new WeatherCodeModel() { Description = "Partly cloudy", WeatherImg = GetWeatherImg("PartlyCloudy.png") },
                WeatherCode.Overcast => new WeatherCodeModel() { Description = "Overcast", WeatherImg = GetWeatherImg("Overcast.png") },
                WeatherCode.Fog => new WeatherCodeModel() { Description = "Fog and depositing rime fog", WeatherImg = GetWeatherImg("Fog.png") },
                WeatherCode.RimeFog => new WeatherCodeModel() { Description = "Fog and depositing rime fog", WeatherImg = GetWeatherImg("RimeFog.png") },
                WeatherCode.DrizzleLight or WeatherCode.DrizzleModerate or WeatherCode.DrizzleDense => new WeatherCodeModel() { Description = "Drizzle: Light intensity", WeatherImg = GetWeatherImg("DrizzleLight.png") },
                WeatherCode.FreezingDrizzleLight or WeatherCode.FreezingDrizzleDense => new WeatherCodeModel() { Description = "Freezing Drizzle: Light intensity", WeatherImg = GetWeatherImg("FreezingDrizzle.png") },
                WeatherCode.RainSlight => new WeatherCodeModel() { Description = "Rain: Slight intensity", WeatherImg = GetWeatherImg("RainSlight.png") },
                WeatherCode.RainModerate => new WeatherCodeModel() { Description = "Rain: Moderate intensity", WeatherImg = GetWeatherImg("RainModerate.png") },
                WeatherCode.RainHeavy => new WeatherCodeModel() { Description = "Rain: Heavy intensity", WeatherImg = GetWeatherImg("RainHeavy.png") },
                WeatherCode.FreezingRainLight => new WeatherCodeModel() { Description = "Freezing Rain: Light intensity", WeatherImg = GetWeatherImg("FreezingRainLight.png") },
                WeatherCode.FreezingRainHeavy => new WeatherCodeModel() { Description = "Freezing Rain: Heavy intensity", WeatherImg = GetWeatherImg("FreezingRainHeavy.png") },
                WeatherCode.SnowFallSlight or WeatherCode.SnowShowersSlight or WeatherCode.RainShowersSlight => new WeatherCodeModel() { Description = "Snow fall: Slight intensity", WeatherImg = GetWeatherImg("SnowSlight.png") },
                WeatherCode.SnowFallModerate or WeatherCode.RainShowersModerate or WeatherCode.SnowGrains => new WeatherCodeModel() { Description = "Snow fall: Moderate intensity", WeatherImg = GetWeatherImg("SnowModerate.png") },
                WeatherCode.SnowFallHeavy or WeatherCode.SnowShowersHeavy or WeatherCode.RainShowersViolent => new WeatherCodeModel() { Description = "Snow fall: Heavy intensity", WeatherImg = GetWeatherImg("SnowHeavy.png") },
                WeatherCode.ThunderstormSlight or WeatherCode.ThunderstormWithSlightHail or WeatherCode.ThunderstormWithHeavyHail => new WeatherCodeModel() { Description = "Thunderstorm: Slight intensity", WeatherImg = GetWeatherImg("Thunderstorm.png") },
                _ => new WeatherCodeModel() { Description = "Unknown Weather Code", WeatherImg = null },
            };
        }
    }
}
