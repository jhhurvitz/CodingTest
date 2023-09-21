using Weather.Domain;

namespace Namespace;
public class OpenWeatherWeatherRepository : AbstractWeatherRepository
{
    private OpenWeatherWeatherClient _client;
    public OpenWeatherWeatherRepository(IGeoCoder geoCoder, OpenWeatherWeatherClient client) :base(geoCoder)
    {
        _client = client;
    }

    protected override Task<WeatherReport> GetWeatherReportFromCordinates(Cordinates coordinates)
    {
        return _client.GetWeatherReport(coordinates);
    }

}
