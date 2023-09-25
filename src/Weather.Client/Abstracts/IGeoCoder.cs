using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.Abstracts;

/// <summary>
/// Interface used for classes that turn a location object into a set of coordinates
/// </summary>
public interface IGeoCoder
{
    /// <summary>
    /// Takes a ciyt, state and/or zip and turns it into coordinates (lat,lon)). 
    /// </summary>
    /// <param name="location"></param>
    /// <returns>Task of cooridnates (lat,lon)</returns>
    Task<Coordinates> GetCoordinates(Location location);
}
