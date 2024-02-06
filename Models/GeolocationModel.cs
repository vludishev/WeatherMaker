using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMaker.Models
{
    public class GeolocationModel
    {
        public List<GeoInfo> Geonames { get; set; }
    }

    public class GeoInfo
    {
        public string Lng { get; set; }
        public int GeonameId { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string LocalizedValue { get; set; }

    }
}
