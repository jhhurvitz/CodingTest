using Weather.Domain;

namespace Namespace;
public class OpenWeatherWeatherClient
{
    private readonly HttpClient _client;
    public OpenWeatherWeatherClient(HttpClient client)
    {
        _client = client;
    }
    public Task<WeatherReport> GetWeatherReport(Cordinates location)
    {
        throw new NotImplementedException();
    }

    public Task GetCordinates(Location location)
    {
        throw new NotImplementedException();
    }
}
