using System.Windows.Media;

namespace WeatherMaker.Models
{
    public class DowTileModel
    {
        public ImageSource? WeatherPicture { get; set; }
        public string? DayOfWeek { get; set; }
        public string? DegreesNumber { get; set; }
    }
}
