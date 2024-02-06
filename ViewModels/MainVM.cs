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
    public class MainVM : INotifyPropertyChanged
    {
        public ObservableCollection<DowTile> DowTiles { get; } = new ObservableCollection<DowTile>();

        public ImageSource WeatherImage { get; set; }
        public string CurrentTemperature { get; set; }
        public string CurrentCity { get; set; }
        public string PrecipitationType { get; set; }
        public string Coordinates { get; set; }

        private TemperatureUnit _temperatureUnit {  get; set; }
        private IWeatherService _weatherService { get; set; }

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

        public MainVM()
        {
            _weatherService = new WeatherService();
            GetCurrentWeatherData();
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

        private void SetHourlyData()
        {
            var weatherData = _weatherService.GetWeatherData<HourlyWeatherResponse>
                (
                    AppSettings.Latitude,
                    AppSettings.Longitude
                ).Result;
            if (weatherData == null)
            {
                return;
            }

            for (int i = 1; i < weatherData.Hourly.Temperature.Count; i++)
            {
                var img = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Hourly.WeatherCode[i]).WeatherImg;

                DowTile dowTile = new()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 70,
                    ViewModel = new()
                    {
                        DayOfWeek = AppSettings.Language == "English" ? DateTime.Parse(weatherData.Hourly.Date[i]).ToString("h tt", CultureInfo.InvariantCulture)
                            : DateTime.Parse(weatherData.Hourly.Date[i]).ToString("HH:mm"),
                        DegreesNumber = $"{Convert.ToInt32(Helper.CalculateTemperature(weatherData.Hourly.Temperature[i], _temperatureUnit))}°",
                        WeatherPicture = img
                    }
                };
                DowTiles.Add(dowTile);
            }
        }

        private void SetDailyData()
        {
            var weatherData = _weatherService.GetWeatherData<DailyWeatherResponse>
                 (
                     AppSettings.Latitude,
                     AppSettings.Longitude
                 ).Result;

            for (int i = 1; i < weatherData.Daily.AverageTemperatures.Count; i++)
            {
                var img = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Daily.WeatherCode[i]).WeatherImg;

                DowTile dowTile = new()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 70,
                    ViewModel = new()
                    {
                        DayOfWeek = DateTime.Parse(weatherData.Daily.Date[i]).ToString("ddd", CultureInfo.CurrentCulture),
                        DegreesNumber = $"{Convert.ToInt32(Helper.CalculateTemperature(weatherData.Daily.AverageTemperatures[i], _temperatureUnit))}°",
                        WeatherPicture = img
                    }
                };
                DowTiles.Add(dowTile);
            }
        }

        private void GetCurrentWeatherData()
        {
            var latitude = AppSettings.Latitude;
            var longitude = AppSettings.Longitude;
            if (Enum.TryParse(AppSettings.TemperatureUnit, out TemperatureUnit temperatureUnit))
            {
                _temperatureUnit = temperatureUnit;
            }
            else
            {
                _temperatureUnit = TemperatureUnit.Celsius;
            }
            

            var weatherData = _weatherService.GetWeatherData<CurrentWeatherResponse>
                  (
                      latitude,
                      longitude
                  ).Result;

            var weatherDescription = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Current.WeatherCode);

            CurrentTemperature = $"{Convert.ToInt32(Helper.CalculateTemperature(weatherData.Current.Temperature, _temperatureUnit))}°";
            WeatherImage = weatherDescription.WeatherImg;
            PrecipitationType = weatherDescription.Description;
            CurrentCity = _weatherService.GetCityName(AppSettings.GeonameId, CultureInfo.CurrentCulture.TwoLetterISOLanguageName).Result;
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
