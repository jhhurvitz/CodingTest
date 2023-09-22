using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.Abstracts;
public interface IGeoCoder
{
    Task<Cordinates> GetCordinates(Location location);
}
