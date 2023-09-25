using Weather.Domain;

namespace Weather.Client.Abstracts;


/// <summary>
/// Interface for classes that take locations (city,state / zip) into a weather report object. 
/// </summary>
public interface IWeatherRepository
{   
    /// <summary>
    /// Takes a location and get the weather for that location. 
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    Task<WeatherReport> GetWeatherReport(Location location);
}
