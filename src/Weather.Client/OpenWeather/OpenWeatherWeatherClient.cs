using System.Net.Http.Json;
using System.Text;
using Weather.Client.Exceptions;
using Weather.Client.Models;
using Weather.Client.OpenWeather.Models;
using Weather.Domain;

namespace Weather.Client.OpenWeather;
public class OpenWeatherWeatherClient
{
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly string _units;

    public OpenWeatherWeatherClient(HttpClient client,WeatherConfiguration configuration, string units = "imperial")
    {
        _client = client;
        _apiKey = configuration.ApiKey;
        _units = units;
    }
    public async Task<WeatherResponse> GetWeatherReport(Coordinates cordinates)
    {
        return await MakeJsonGetRequest<WeatherResponse>("data/2.5/forecast",new Dictionary<string, string>(){
            {"lat",$"{cordinates.latitude}"},
            {"lon",$"{cordinates.longitude}"},
            {"units",_units}
        });
    }

    public async Task<IEnumerable<GeoLocationResult>> GetCordinates(Location location)
    {
        return await MakeJsonGetRequest<IEnumerable<GeoLocationResult>>( "geo/1.0/direct",new Dictionary<string, string>(){
            {"q",$"{location.City},{location.State},{location.CountryCode}"}
        });
        
    }

    private async Task<T> MakeJsonGetRequest<T>(string path,Dictionary<string,string> parameters)
    {   
        parameters ??= new Dictionary<string, string>();

        parameters.Add("appid",_apiKey);

        var queryString = string.Empty;
     
        var stringParams = string.Join('&',parameters.Select(param => $"{param.Key}={param.Value}"));
        queryString = $"?{stringParams}";
         

   
            var response = await _client.GetAsync($"/{path}{queryString}");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                var content = await response.Content.ReadFromJsonAsync<Dictionary<string,object>>();
                StringBuilder msg = new StringBuilder();

                switch(response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        msg.AppendLine("bad parameters passed to weather api");
                        break;
                    case System.Net.HttpStatusCode.TooManyRequests:
                        msg.AppendLine("exceeded number of allowed request by weather api");
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        msg.AppendLine("unauthorized exception this is likely due to an expired or disabled api key (note that api keys take an hour to take effect)");
                        break;
                    default:
                        msg = msg.AppendLine("Unknown API exepection has occured");
                        break;
                }
                
                if(content?.TryGetValue("message",out var message) ?? false)
                {
                    msg.AppendLine("Additional Details:");
                    msg.AppendLine(message.ToString());
                }
                
                throw new DataRetrievalException(msg.ToString(),null);

            }
        
    }
}
