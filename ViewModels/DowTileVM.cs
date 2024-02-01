using System.ComponentModel;
using System.Windows.Media;
using WeatherMaker.Models;

namespace WeatherMaker.ViewModels
{
    public class DowTileVM : INotifyPropertyChanged
    {
        public DowTileModel DowTileModel;

        public ImageSource? WeatherPicture
        {
            get { return DowTileModel.WeatherPicture; }
            set
            {
                if (DowTileModel.WeatherPicture != value)
                {
                    DowTileModel.WeatherPicture = value!;
                    OnPropertyChanged(nameof(WeatherPicture));
                }
            }
        }

        public string? DayOfWeek
        {
            get { return DowTileModel.DayOfWeek; }
            set
            {
                if (DowTileModel.DayOfWeek != value)
                {
                    DowTileModel.DayOfWeek = value!;
                    OnPropertyChanged(nameof(DayOfWeek));
                }
            }
        }

        public string? DegreesNumber
        {
            get { return DowTileModel.DegreesNumber; }
            set
            {
                if (DowTileModel.DegreesNumber != value)
                {
                    DowTileModel.DegreesNumber = value!;
                    OnPropertyChanged(nameof(DegreesNumber));
                }
            }
        }

        public DowTileVM()
        {
            DowTileModel = new DowTileModel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
