using System.Text.Json.Serialization;

namespace Weather.Client.OpenWeather.Models;

/// <summary>
/// Data transfer object that represents the result of a call to the openweather geocoding api
/// </summary>
public class GeoLocationResult
{
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }
    [JsonPropertyName("lon")]
    public decimal Longitude { get; set; }
}
