using System.Text.Json.Serialization;

namespace Weather.Client.OpenWeather.Models;
public class GeoLocationResult
{
    public string Name {get;set;} = string.Empty;
    
    [JsonPropertyName("lat")]
    public decimal Latitude {get;set;}
    [JsonPropertyName("lon")]
    public decimal Longitude {get;set;}
}
