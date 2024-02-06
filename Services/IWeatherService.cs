using WeatherMaker.Models;

namespace WeatherMaker.Services
{
    public interface IWeatherService
    {
        Task<T?> GetWeatherData<T>(string lat, string lng);
        Task<T?> GetAllCityNames<T>();
        Task<string> GetCityName(string geonameId, string lang);
    }
}
