using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMaker.Models
{
    public class WeatherCache<T>
    {
        public T CachedData { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
