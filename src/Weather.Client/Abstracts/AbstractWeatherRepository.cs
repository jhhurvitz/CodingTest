using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.Abstracts;
public abstract class AbstractWeatherRepository : IWeatherRepository
{
    private readonly IGeoCoder _geoCoder;
    public AbstractWeatherRepository(IGeoCoder geoCoder) => _geoCoder = geoCoder;

    private async Task<Coordinates> GetGeoCode(Location location)
    {
        return await _geoCoder.GetCoordinates(location);
    }
    public async Task<WeatherReport> GetWeatherReport(Location location)
    {
        var cordinates = await GetGeoCode(location);

        return await GetWeatherReportFromCordinates(cordinates,location);

    }

     protected abstract Task<WeatherReport> GetWeatherReportFromCordinates(Coordinates coordinates,Location location);
}
