namespace Weather.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Weather.Client.Abstracts;
using Weather.Client.OpenWeather;

public record WeatherConfiguration (string Url, string ApiKey);


public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddWeatherRepository(this IServiceCollection collection,WeatherConfiguration weatherConfiguration)
    {
        collection.AddSingleton(weatherConfiguration);
         collection.AddHttpClient<OpenWeatherWeatherClient>(client=>client.BaseAddress= new Uri(weatherConfiguration.Url));
         collection.AddSingleton<IGeoCoder,OpenWeatherGeoCoder>();
         collection.AddSingleton<IWeatherRepository,OpenWeatherWeatherRepository>();

         return collection;
    }
}
