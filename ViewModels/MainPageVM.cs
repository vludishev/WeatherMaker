using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using WeatherMaker.Models;
using WeatherMaker.Models.Responses;
using WeatherMaker.Services;

namespace WeatherMaker.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<DowTile> DowTiles { get; } = [];

        public ImageSource WeatherImage { get; set; }
        public string CurrentTemperature { get; set; }
        public string CurrentCity { get; set; }
        public string PrecipitationType { get; set; }
        public string Coordinates { get; set; }

        private TemperatureUnit TemperatureUnit {  get; set; }
        private IWeatherService WeatherService { get; set; }

        private readonly string latitude;
        private readonly string longitude;

        private RelayCommand dailyModeCommand;
        private RelayCommand hourlyModeCommand;

        public RelayCommand DailyModeCommand
        {
            get
            {
                return dailyModeCommand ?? (dailyModeCommand = new RelayCommand(DailyModeCommandExec));
            }
        }

        public RelayCommand HourlyModeCommand
        {
            get
            {
                return hourlyModeCommand ?? (hourlyModeCommand = new RelayCommand(HourlyModeCommandExec));
            }
        }

        public MainPageVM()
        {
            WeatherService = new WeatherService();

            latitude = AppSettings.Latitude;
            longitude = AppSettings.Longitude;

            SetCurrentWeatherData();
            SetDailyData();
        }

        private void DailyModeCommandExec(object obj)
        {
            DowTiles.Clear();
            SetDailyData();
        }

        private void HourlyModeCommandExec(object obj)
        {
            DowTiles.Clear();
            SetHourlyData();
        }

        public void SetHourlyData()
        {
            HourlyWeatherResponse? weatherData = WeatherCache.GetData<HourlyWeatherResponse>(nameof(HourlyWeatherResponse));

            if (weatherData == null)
            {
                weatherData = WeatherService.GetWeatherData<HourlyWeatherResponse>
                  (
                      latitude,
                      longitude
                  ).Result;

                WeatherCache.SetData(nameof(HourlyWeatherResponse), weatherData, DateTimeOffset.UtcNow.AddHours(1));
            }

            for (int i = 1; i < weatherData.Hourly.Temperature.Count; i++)
            {
                var img = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Hourly.WeatherCode[i]).WeatherImg;

                DowTile dowTile = new()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 67,
                    ViewModel = new()
                    {
                        DayOfWeek = AppSettings.Language == "English" ? DateTime.Parse(weatherData.Hourly.Date[i]).ToString("h tt", CultureInfo.InvariantCulture)
                            : DateTime.Parse(weatherData.Hourly.Date[i]).ToString("HH:mm"),
                        DegreesNumber = $"{Convert.ToInt32(Helper.CalculateTemperature(weatherData.Hourly.Temperature[i], TemperatureUnit))}°",
                        WeatherPicture = img
                    }
                };
                DowTiles.Add(dowTile);
            }
        }

        public void SetDailyData()
        {
            DailyWeatherResponse? weatherData = WeatherCache.GetData<DailyWeatherResponse>(nameof(DailyWeatherResponse));

            if (weatherData == null)
            {
                weatherData = WeatherService.GetWeatherData<DailyWeatherResponse>
                  (
                      latitude,
                      longitude
                  ).Result;

                WeatherCache.SetData(nameof(DailyWeatherResponse), weatherData, DateTimeOffset.UtcNow.AddHours(1));
            }

            for (int i = 1; i < weatherData.Daily.AverageTemperatures.Count; i++)
            {
                var img = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Daily.WeatherCode[i]).WeatherImg;

                DowTile dowTile = new()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 67,
                    ViewModel = new()
                    {
                        DayOfWeek = DateTime.Parse(weatherData.Daily.Date[i]).ToString("ddd", CultureInfo.CurrentCulture),
                        DegreesNumber = $"{Convert.ToInt32(Helper.CalculateTemperature(weatherData.Daily.AverageTemperatures[i], TemperatureUnit))}°",
                        WeatherPicture = img
                    }
                };
                DowTiles.Add(dowTile);
            }
        }

        public void SetCurrentWeatherData()
        {
            var latitude = AppSettings.Latitude;
            var longitude = AppSettings.Longitude;
            if (Enum.TryParse(AppSettings.TemperatureUnit, out TemperatureUnit temperatureUnit))
            {
                TemperatureUnit = temperatureUnit;
            }
            else
            {
                TemperatureUnit = TemperatureUnit.Celsius;
            }

            CurrentWeatherResponse? weatherData = WeatherCache.GetData<CurrentWeatherResponse>(nameof(CurrentWeatherResponse));

            if (weatherData == null)
            {
                weatherData = WeatherService.GetWeatherData<CurrentWeatherResponse>
                  (
                      latitude,
                      longitude
                  ).Result;

                WeatherCache.SetData(nameof(CurrentWeatherResponse), weatherData, DateTimeOffset.UtcNow.AddHours(1));
            }

            var weatherDescription = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Current.WeatherCode);

            CurrentTemperature = $"{Convert.ToInt32(Helper.CalculateTemperature(weatherData.Current.Temperature, TemperatureUnit))}°";
            WeatherImage = weatherDescription.WeatherImg;
            PrecipitationType = weatherDescription.Description;
            CurrentCity = WeatherService.GetCityName(AppSettings.GeonameId, CultureInfo.CurrentCulture.TwoLetterISOLanguageName).Result;
            Coordinates = $"{LocalizedLogic.Instance["LatitudeTB"]}:{Convert.ToInt32(double.Parse(latitude, CultureInfo.GetCultureInfo("en")))}° " +
                $"{LocalizedLogic.Instance["LongitudeTB"]}:{Convert.ToInt32(double.Parse(longitude, CultureInfo.GetCultureInfo("en")))}°";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
