using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.Abstracts;
public abstract class AbstractWeatherRepository : IWeatherRepository
{
    private readonly IGeoCoder _geoCoder;
    public AbstractWeatherRepository(IGeoCoder geoCoder) => _geoCoder = geoCoder;

    private async Task<Cordinates> GetGeoCode(Location location)
    {
        return await _geoCoder.GetCordinates(location);
    }
    public async Task<WeatherReport> GetWeatherReport(Location location)
    {
        var cordinates = await GetGeoCode(location);

        return await GetWeatherReportFromCordinates(cordinates,location);

    }

     protected abstract Task<WeatherReport> GetWeatherReportFromCordinates(Cordinates coordinates,Location location);
}
