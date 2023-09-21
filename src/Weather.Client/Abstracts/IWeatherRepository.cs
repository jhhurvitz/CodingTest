using Weather.Domain;

namespace Namespace;
public interface IWeatherRepository
{   
    Task<WeatherReport> GetWeatherReport(Location location);
}
