using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using WeatherMaker.Services;

namespace WeatherMaker.Models
{
    public class WeatherCache
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;

        public static T? GetData<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            // Пытаемся получить данные из кэша
            if (_cache.Contains(key))
                return (T)_cache.Get(key);

            return default;
        }

        public static void SetData<T>(string key, T weatherData, DateTimeOffset expirationTime)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }
            else if (weatherData == null)
            {
                throw new ArgumentException("WeatherData cannot be null or empty.", nameof(weatherData));
            }

            _cache.Set(key, weatherData, expirationTime);
        }
    }
}
