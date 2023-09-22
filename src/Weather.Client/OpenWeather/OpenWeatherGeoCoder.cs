
using System.Text.RegularExpressions;
using Weather.Client.Abstracts;
using Weather.Client.Models;

namespace Weather.Client.OpenWeather;
public class OpenWeatherGeoCoder : IGeoCoder
{

    private readonly OpenWeatherWeatherClient _client;
    public OpenWeatherGeoCoder(OpenWeatherWeatherClient client)=> _client = client;
    public async Task<Coordinates> GetCoordinates(Location location)
    {
       var coordinates = await _client.GetCordinates(location);

       var result = coordinates?.FirstOrDefault();

       return new Coordinates(result.Latitude,result.Longitude);
    }
}
