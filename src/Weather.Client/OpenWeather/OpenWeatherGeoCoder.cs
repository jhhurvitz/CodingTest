
using System.Text.RegularExpressions;
using Weather.Client.Abstracts;
using Weather.Client.Models;

namespace Weather.Client.OpenWeather;
public class OpenWeatherGeoCoder : IGeoCoder
{

    private readonly OpenWeatherWeatherClient _client;
    public OpenWeatherGeoCoder(OpenWeatherWeatherClient client)=> _client = client;
    public async Task<Cordinates> GetCordinates(Location location)
    {
       var cordinates = await _client.GetCordinates(location);

       var result =  cordinates.FirstOrDefault();

       return new Cordinates(result.Latitude,result.Longitude);
    }
}
