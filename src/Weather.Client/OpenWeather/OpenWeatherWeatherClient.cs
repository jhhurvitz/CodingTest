using System.Net.Http.Json;
using System.Text;
using Weather.Client.Exceptions;
using Weather.Client.Models;
using Weather.Client.OpenWeather.Models;
using Weather.Domain;

namespace Weather.Client.OpenWeather;

/// <summary>
/// Class used to retrieve weather and geographic data from openweather
/// </summary>
public class OpenWeatherWeatherClient
{
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly string _units;

    /// <summary>
    /// Contstructs a OpenWeatherClient
    /// </summary>
    /// <param name="client">Expecting a httpclient with the base address to openweather definied</param>
    /// <param name="configuration">OpenWeather Configuration</param>
    /// <param name="units"> Units to be used for rendering Imperial , Metric. imperial is default</param>
    public OpenWeatherWeatherClient(HttpClient client, WeatherConfiguration configuration, string units = "imperial")
    {
        _client = client;
        _apiKey = configuration.ApiKey;
        _units = units;
    }


    /// <summary>
    /// Returns a weather Report object for the given coordinates from the 5 day / 3 hours endpoint  https://openweathermap.org/forecast5
    /// </summary>
    /// <param name="cordinates"></param>
    /// <returns>Return a WeatherResponse https://openweathermap.org/forecast5#fields_JSON </returns>
    public async Task<WeatherResponse> GetWeatherReport(Coordinates cordinates)
    {
        //have to use lat lon OpenWeather  are deprecating endpoints that use city,state zip in a move towards the geocoding api https://openweathermap.org/forecast5#geocoding
        return await MakeJsonGetRequest<WeatherResponse>("data/2.5/forecast", new Dictionary<string, string>(){
            {"lat",$"{cordinates.Latitude}"},
            {"lon",$"{cordinates.Longitude}"},
            {"units",_units}
        });
    }

    /// <summary>
    /// Return currents from OpenWeather GeoCoding API 
    /// </summary>
    /// <param name="location"></param>
    /// <returns>Returns the first geolocation for ethier a zip code if provided or a city and state https://openweathermap.org/api/geocoding-api#direct_name_fields </returns>
    public async Task<GeoLocationResult> GetCordinates(Location location)
    {
        if (location.ZipCode == null)
        {
            var result = await MakeJsonGetRequest<IEnumerable<GeoLocationResult>>("geo/1.0/direct", new Dictionary<string, string>(){
            {"q",$"{location.City},{location.State},{location.CountryCode}"}
        });

            return result.FirstOrDefault();
        }
        else
        {
            return await MakeJsonGetRequest<GeoLocationResult>("geo/1.0/zip", new Dictionary<string, string>(){
            {"zip",$"{location.ZipCode},{location.CountryCode}"}
            });
        }


    }

    /// <summary>
    /// Generic Method fro making get requests from open weather
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">Path to Api Endpoint</param>
    /// <param name="parameters">Query String parameters api key will be appended</param>
    /// <returns>Returns T Result where T is the json deserilized from api response</returns>
    /// <exception cref="DataRetrievalException"></exception>
    private async Task<T> MakeJsonGetRequest<T>(string path, Dictionary<string, string> parameters)
    {
        parameters ??= new Dictionary<string, string>();

        parameters.Add("appid", _apiKey);

        var queryString = string.Empty;

        var stringParams = string.Join('&', parameters.Select(param => $"{param.Key}={param.Value}"));
        queryString = $"?{stringParams}";



        var response = await _client.GetAsync($"/{path}{queryString}");

        if (response.IsSuccessStatusCode)
        {

            return await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            StringBuilder msg = new StringBuilder();

            switch (response.StatusCode)
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

            var content = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            if (content?.TryGetValue("message", out var message) ?? false)
            {
                msg.AppendLine("Additional Details:");
                msg.AppendLine(message.ToString());
            }

            throw new DataRetrievalException(msg.ToString(), null);

        }

    }
}
