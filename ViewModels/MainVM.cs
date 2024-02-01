using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using WeatherMaker.Models;
using WeatherMaker.Services;

namespace WeatherMaker.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        public ObservableCollection<DowTile> DowTiles { get; } = new ObservableCollection<DowTile>();

        public ImageSource WeatherImage { get; set; }
        public double CurrentTemperature { get; set; }
        public string CurrentCity { get; set; }
        public string PrecipitationType { get; set; }
        public string Coordinates { get; set; }

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
            var weatherData = _weatherService.GetWeatherData<HourlyWeatherResponse>().Result;
            if (weatherData == null)
            {
                return;
            }

            for (int i = 1; i < weatherData.Hourly.Time.Count; i++)
            {
                var img = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Hourly.Weather_code[i]).WeatherImg;

                DowTile dowTile = new()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 70,
                    ViewModel = new()
                    {
                        DayOfWeek = DateTime.Parse(weatherData.Hourly.Time[i]).ToString("h tt", CultureInfo.InvariantCulture),
                        DegreesNumber = $"{(int)weatherData.Hourly.Temperature_2m[i]}°C",
                        WeatherPicture = img
                    }
                };
                DowTiles.Add(dowTile);
            }
        }

        private void SetDailyData()
        {
            var weatherData = _weatherService.GetWeatherData<WeatherDataDailyModel>().Result;
            if (weatherData == null)
            {
                return;
            }

            for (int i = 1; i < weatherData.Daily.Temperature_2m_max.Count; i++)
            {
                var img = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Daily.Weather_code[i]).WeatherImg;

                DowTile dowTile = new()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 70,
                    ViewModel = new()
                    {
                        DayOfWeek = DateTime.Parse(weatherData.Daily.Time[i]).ToString("ddd"),
                        DegreesNumber = $"{(int)weatherData.Daily.Temperature_2m_max[i]}°",
                        WeatherPicture = img
                    }
                };
                DowTiles.Add(dowTile);
            }
        }

        private void GetCurrentWeatherData()
        {
            var weatherData = _weatherService.GetWeatherData<CurrentWeatherDataModel>().Result;
            if (weatherData == null)
            {
                return;
            }

            var weatherDescription = WeatherCodeDescriptions.GetDescription((WeatherCode)weatherData.Current.WeatherCode);

            CurrentTemperature = weatherData.Current.Temperature_2m;
            WeatherImage = weatherDescription.WeatherImg;
            PrecipitationType = weatherDescription.Description;
            CurrentCity = _weatherService.GetCurrentCity(weatherData.Latitude, weatherData.Longitude).Result;
            Coordinates = $"H:{(int)weatherData.Latitude}° L:{(int)weatherData.Longitude}°";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
