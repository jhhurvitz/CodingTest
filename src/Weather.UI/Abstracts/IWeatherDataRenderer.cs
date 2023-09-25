namespace Weather.UI.Abstracts;
public interface IWeatherDataRenderer
{
    void AddLUpdateLocation(Location location);
    void AddUpdateLocations(IEnumerable<Location> locations);
    Task Render();
}
