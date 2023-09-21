using Weather.Domain;

namespace Namespace;
public interface IGeoCoder
{
    Task<Cordinates> GetCordinates(Location location);
}
