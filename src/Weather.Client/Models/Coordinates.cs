namespace Weather.Client.Models;

/// <summary>
/// Record that represents a set of coordinates 
/// </summary>
/// <param name="Latitude">Latiude in decimal form </param>
/// <param name="Longitude">Longitude represented as a decimal</param>
public record class Coordinates(decimal Latitude, decimal Longitude);