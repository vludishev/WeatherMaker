namespace WeatherMaker.Services
{
    public interface IWeatherService
    {
        Task<T?> GetWeatherData<T>();
        Task<string> GetCurrentCity(double latitude, double longitude);
    }
}
