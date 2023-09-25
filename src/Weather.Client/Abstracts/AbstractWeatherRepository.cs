using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.Abstracts;

/// <summary>
/// Abstract class that implments IWeatherRepository handling the goecoding logic
/// </summary>
public abstract class AbstractWeatherRepository : IWeatherRepository
{
    private readonly IGeoCoder _geoCoder;
    public AbstractWeatherRepository(IGeoCoder geoCoder) => _geoCoder = geoCoder;

/// <summary>
/// Using the goecoder implmnetation passed to the constructor gets a set of coordinates from a city, state and/or zip code
/// </summary>
/// <param name="location"></param>
/// <returns>Task of Coordinate</returns>
    private async Task<Coordinates> GetGeoCode(Location location)
    {
        return await _geoCoder.GetCoordinates(location);
    }

    /// <summary>
    /// Get A weather Report given a locatiion
    /// </summary>
    /// <param name="location">Location</param>
    /// <returns>A task that returns a WeatherReport</returns>
    public async Task<WeatherReport> GetWeatherReport(Location location)
    {
        var Coordinates = await GetGeoCode(location);

        return await GetWeatherReportFromCoordinates(Coordinates,location);

    }

     protected abstract Task<WeatherReport> GetWeatherReportFromCoordinates(Coordinates coordinates,Location location);
}
