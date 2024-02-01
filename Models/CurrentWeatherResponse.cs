public class CurrentUnits
{
    public string Time { get; set; }
    public string Interval { get; set; }
    public string Temperature_2m { get; set; }
    public string WeatherCode { get; set; }
}

public class CurrentWeather
{
    public string Time { get; set; }
    public int Interval { get; set; }
    public double Temperature_2m { get; set; }
    public int WeatherCode { get; set; }
}

public class CurrentWeatherDataModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationTime_ms { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }
    public CurrentUnits CurrentUnits { get; set; }
    public CurrentWeather Current { get; set; }
}
