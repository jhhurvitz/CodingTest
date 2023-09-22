using Weather.Domain;

namespace Weather.Client.Abstracts;
public interface IWeatherRepository
{   
    Task<WeatherReport> GetWeatherReport(Location location);
}
