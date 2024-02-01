
public class DailyUnits
{
    public string Time { get; set; }
    public string WeatherCode { get; set; }
    public string Temperature_2m_max { get; set; }
    public string Temperature_2m_min { get; set; }
}

public class DailyData
{
    public List<string> Time { get; set; }
    public List<int> Weather_code { get; set; }
    public List<double> Temperature_2m_max { get; set; }
    public List<double> Temperature_2m_min { get; set; }
}

public class WeatherDataDailyModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationTime_ms { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; }
    public string TimezoneAbbreviation { get; set; }
    public double Elevation { get; set; }
    public DailyUnits DailyUnits { get; set; }
    public DailyData Daily { get; set; }
}