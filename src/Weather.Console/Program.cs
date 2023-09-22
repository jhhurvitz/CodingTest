// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Weather.Client;
using Weather.UI;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWeatherRepository(new WeatherConfiguration("https://api.openweathermap.org/",args[0]));
builder.Services.AddSingleton<UIRenderer>();
using IHost host = builder.Build();

var ui = host.Services.GetRequiredService<UIRenderer>();

ui.AddLocations(new List<Location>(){ 
    new Location("Marlboro","MA"),
    new Location("San Diego"," CA"),
    new Location("Cheyenne","WY"),
    new Location("Anchorage","AK"),
    new Location("Austin","TX"),
    new Location("Orlando"," FL"),
    new Location("Seattle","WA"),
    new Location("Cleveland","OH"),
    new Location("Portland","ME"),
    new Location("Honolulu","HI"),
    });

await ui.Render();