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
        ThunderstormWithHeavyHail = 99,
        Default = 100
    }

    public class WeatherCodeModel
    {
        public string Description { get; set; }
        public ImageSource? WeatherImg { get; set; }
    }

    public static class WeatherCodeDescriptions
    {
        public static ImageSource GetWeatherImg(string fileName)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/WeatherMaker;component/resources/images/{fileName}"));
        }

        public static WeatherCodeModel GetDescription(WeatherCode code)
        {
            return code switch
            {
                WeatherCode.ClearSky => new WeatherCodeModel() { Description = LocalizedLogic.Instance["ClearSky"], WeatherImg = GetWeatherImg("ClearSky.png") },
                WeatherCode.MainlyClear => new WeatherCodeModel() { Description = LocalizedLogic.Instance["MainlyClear"], WeatherImg = GetWeatherImg("MainlyClear.png") },
                WeatherCode.PartlyCloudy => new WeatherCodeModel() { Description = LocalizedLogic.Instance["PartlyCloudy"], WeatherImg = GetWeatherImg("PartlyCloudy.png") },
                WeatherCode.Overcast => new WeatherCodeModel() { Description = LocalizedLogic.Instance["Overcast"], WeatherImg = GetWeatherImg("Overcast.png") },
                WeatherCode.Fog => new WeatherCodeModel() { Description = LocalizedLogic.Instance["Fog"], WeatherImg = GetWeatherImg("Fog.png") },
                WeatherCode.RimeFog => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RimeFog"], WeatherImg = GetWeatherImg("RimeFog.png") },
                WeatherCode.DrizzleLight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["DrizzleLight"], WeatherImg = GetWeatherImg("DrizzleLight.png") },
                WeatherCode.DrizzleModerate => new WeatherCodeModel() { Description = LocalizedLogic.Instance["DrizzleModerate"], WeatherImg = GetWeatherImg("DrizzleLight.png") },
                WeatherCode.DrizzleDense => new WeatherCodeModel() { Description = LocalizedLogic.Instance["DrizzleDense"], WeatherImg = GetWeatherImg("DrizzleLight.png") },
                WeatherCode.FreezingDrizzleLight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["FreezingDrizzleLight"], WeatherImg = GetWeatherImg("FreezingDrizzle.png") },
                WeatherCode.FreezingDrizzleDense => new WeatherCodeModel() { Description = LocalizedLogic.Instance["FreezingDrizzleDense"], WeatherImg = GetWeatherImg("FreezingDrizzle.png") },
                WeatherCode.RainSlight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RainSlight"], WeatherImg = GetWeatherImg("RainSlight.png") },
                WeatherCode.RainModerate => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RainModerate"], WeatherImg = GetWeatherImg("RainModerate.png") },
                WeatherCode.RainHeavy => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RainHeavy"], WeatherImg = GetWeatherImg("RainHeavy.png") },
                WeatherCode.FreezingRainLight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["FreezingRainLight"], WeatherImg = GetWeatherImg("FreezingRainLight.png") },
                WeatherCode.FreezingRainHeavy => new WeatherCodeModel() { Description = LocalizedLogic.Instance["FreezingRainHeavy"], WeatherImg = GetWeatherImg("FreezingRainHeavy.png") },
                WeatherCode.SnowFallSlight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["SnowFallSlight"], WeatherImg = GetWeatherImg("FreezingRainHeavy.png") },
                WeatherCode.SnowShowersSlight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["SnowShowersSlight"], WeatherImg = GetWeatherImg("FreezingRainHeavy.png") },
                WeatherCode.RainShowersSlight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RainShowersSlight"], WeatherImg = GetWeatherImg("SnowSlight.png") },
                WeatherCode.SnowFallModerate => new WeatherCodeModel() { Description = LocalizedLogic.Instance["SnowFallModerate"], WeatherImg = GetWeatherImg("SnowSlight.png") },
                WeatherCode.RainShowersModerate => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RainShowersModerate"], WeatherImg = GetWeatherImg("SnowSlight.png") },
                WeatherCode.SnowGrains => new WeatherCodeModel() { Description = LocalizedLogic.Instance["SnowGrains"], WeatherImg = GetWeatherImg("SnowModerate.png") },
                WeatherCode.SnowFallHeavy => new WeatherCodeModel() { Description = LocalizedLogic.Instance["SnowFallHeavy"], WeatherImg = GetWeatherImg("SnowModerate.png") },
                WeatherCode.SnowShowersHeavy => new WeatherCodeModel() { Description = LocalizedLogic.Instance["SnowShowersHeavy"], WeatherImg = GetWeatherImg("SnowModerate.png") },
                WeatherCode.RainShowersViolent => new WeatherCodeModel() { Description = LocalizedLogic.Instance["RainShowersViolent"], WeatherImg = GetWeatherImg("SnowHeavy.png") },
                WeatherCode.ThunderstormSlight => new WeatherCodeModel() { Description = LocalizedLogic.Instance["ThunderstormSlight"], WeatherImg = GetWeatherImg("SnowHeavy.png") },
                WeatherCode.ThunderstormWithSlightHail => new WeatherCodeModel() { Description = LocalizedLogic.Instance["ThunderstormWithSlightHail"], WeatherImg = GetWeatherImg("SnowHeavy.png") },
                WeatherCode.ThunderstormWithHeavyHail => new WeatherCodeModel() { Description = LocalizedLogic.Instance["ThunderstormWithHeavyHail"], WeatherImg = GetWeatherImg("Thunderstorm.png") },
                _ => new WeatherCodeModel() { Description = "Unknown Weather Code", WeatherImg = null },
            };
        }
    }
}
