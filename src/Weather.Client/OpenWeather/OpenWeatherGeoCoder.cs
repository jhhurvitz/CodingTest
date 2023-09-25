
using System.Text.RegularExpressions;
using Weather.Client.Abstracts;
using Weather.Client.Models;

namespace Weather.Client.OpenWeather;


/// <summary>
/// GoeCoder using the Version 1 of the goecoding api.
/// </summary>
public class OpenWeatherGeoCoder : IGeoCoder
{

    private readonly OpenWeatherWeatherClient _client;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="client">OpenWeatherClient</param>
    public OpenWeatherGeoCoder(OpenWeatherWeatherClient client)=> _client = client;

    /// <summary>
    /// Get Coordinates given a location using the openweather client. 
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public async Task<Coordinates> GetCoordinates(Location location)
    {
       var coordinates = await _client.GetCordinates(location);

       return new Coordinates(coordinates.Latitude,coordinates.Longitude);
    }
}
