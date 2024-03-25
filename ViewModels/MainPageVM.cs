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

        private TemperatureUnit TemperatureUnit { get; set; }
        private IWeatherService WeatherService { get; set; }

        private readonly HourlyWeatherResponse? HourlyWeatherData;
        private readonly DailyWeatherResponse? DailyWeatherData;
        private readonly CurrentWeatherResponse? CurrentWeatherData;

        private readonly string latitude;
        private readonly string longitude;

        public RelayCommand DailyModeCommand { get; }
        public RelayCommand HourlyModeCommand { get; }

        public MainPageVM()
        {
            WeatherService = new WeatherService();

            latitude = AppSettings.Latitude;
            longitude = AppSettings.Longitude;

            DailyModeCommand = new RelayCommand(DailyModeCommandExec);
            HourlyModeCommand = new RelayCommand(HourlyModeCommandExec);

            HourlyWeatherData = GetWeatherData<HourlyWeatherResponse>("HourlyWeatherResponse");
            DailyWeatherData = GetWeatherData<DailyWeatherResponse>("DailyWeatherResponse");
            CurrentWeatherData = GetWeatherData<CurrentWeatherResponse>("CurrentWeatherResponse");

            SetDailyData();
            SetCurrentWeatherData();
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
            for (int i = 1; i < HourlyWeatherData.Hourly.Temperature.Count; i++)
            {
                var img = GetWeatherCodeDescription((WeatherCode)HourlyWeatherData.Hourly.WeatherCode[i]).WeatherImg;

                DowTiles.Add(new DowTile
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 67,
                    ViewModel = new DowTileVM
                    {
                        DayOfWeek = AppSettings.Language == "English" ? DateTime.Parse(HourlyWeatherData.Hourly.Date[i]).ToString("h tt", CultureInfo.InvariantCulture)
                            : DateTime.Parse(HourlyWeatherData.Hourly.Date[i]).ToString("HH:mm"),
                        DegreesNumber = $"{Convert.ToInt32(Helper.CalculateTemperature(HourlyWeatherData.Hourly.Temperature[i], TemperatureUnit))}°",
                        WeatherPicture = img
                    }
                });
            }
        }

        public void SetDailyData()
        {
            for (int i = 1; i < DailyWeatherData.Daily.AverageTemperatures.Count; i++)
            {
                var img = GetWeatherCodeDescription((WeatherCode)DailyWeatherData.Daily.WeatherCode[i]).WeatherImg;

                DowTiles.Add(new DowTile
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 67,
                    ViewModel = new DowTileVM
                    {
                        DayOfWeek = DateTime.Parse(DailyWeatherData.Daily.Date[i]).ToString("ddd", CultureInfo.CurrentCulture),
                        DegreesNumber = $"{Convert.ToInt32(Helper.CalculateTemperature(DailyWeatherData.Daily.AverageTemperatures[i], TemperatureUnit))}°",
                        WeatherPicture = img
                    }
                });
            }
        }

        private T? GetWeatherData<T>(string cacheKey) where T : class
        {
            T? weatherData = WeatherCache.GetData<T>(cacheKey);

            if (weatherData == null)
            {
                weatherData = WeatherService.GetWeatherData<T>(latitude, longitude).Result;
                WeatherCache.SetData(cacheKey, weatherData, DateTimeOffset.UtcNow.AddHours(1));
            }

            return weatherData;
        }

        private static WeatherCodeModel GetWeatherCodeDescription(WeatherCode code)
        {
            return WeatherCodeDescriptions.GetDescription(code);
        }

        public void SetCurrentWeatherData()
        {
            var weatherDescription = GetWeatherCodeDescription((WeatherCode)CurrentWeatherData.Current.WeatherCode);
            CurrentTemperature = $"{Convert.ToInt32(Helper.CalculateTemperature(CurrentWeatherData.Current.Temperature, TemperatureUnit))}°";
            WeatherImage = weatherDescription.WeatherImg;
            PrecipitationType = weatherDescription.Description;
            CurrentCity = ci
            Coordinates = $"{LocalizedLogic.Instance["LatitudeTB"]}:{Convert.ToInt32(double.Parse(latitude, CultureInfo.GetCultureInfo("en")))}° " +
                $"{LocalizedLogic.Instance["LongitudeTB"]}:{Convert.ToInt32(double.Parse(longitude, CultureInfo.GetCultureInfo("en")))}°";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
