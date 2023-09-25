// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Weather.Client;
using Weather.UI;
using Weather.UI.Abstracts;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWeatherRepository(new WeatherConfiguration("https://api.openweathermap.org/", "{your api key here}"));
builder.Services.AddSingleton<IWeatherDataRenderer, ConsoleWeatherDataRenderer>();
using IHost host = builder.Build();

var ui = host.Services.GetRequiredService<IWeatherDataRenderer>();

ui.AddUpdateLocations(new List<Location>(){
    new Location("Marlboro","MA","01752"),
    new Location("San Diego"," CA","92101"),
    new Location("Cheyenne","WY","82001"),
    new Location("Anchorage","AK","99501"),
    new Location("Austin","TX","78701"),
    new Location("Orlando"," FL","32801"),
    new Location("Seattle","WA","98101"),
    new Location("Cleveland","OH","44113"),
    new Location("Portland","ME","04101"),
    new Location("Honolulu","HI","96813"),
    });

await ui.Render();