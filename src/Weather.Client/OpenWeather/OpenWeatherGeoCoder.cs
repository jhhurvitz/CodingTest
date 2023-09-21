
namespace Namespace;
public class OpenWeatherGeoCoder : IGeoCoder
{

    private readonly OpenWeatherWeatherClient _client;
    public OpenWeatherGeoCoder(OpenWeatherWeatherClient client)=> _client = client;
    public Task<Cordinates> GetCordinates(Location location)
    {
       throw new NotImplementedException();
    }
}
