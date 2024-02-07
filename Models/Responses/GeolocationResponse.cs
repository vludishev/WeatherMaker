using System.Text.Json.Serialization;

namespace WeatherMaker.Models.Responses
{
    public class GeolocationResponse
    {
        public List<GeoInfo> Geonames { get; set; }
    }

    public class GeoInfo
    {
        [JsonPropertyName("Lat")]
        public string? Latitude { get; set; }
        [JsonPropertyName("GeonameId")]
        public int GeonameId { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Lng")]
        public string? Longitude { get; set; }
    }
}
