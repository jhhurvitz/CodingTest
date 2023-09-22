using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Primitives;
using Weather.Client.Abstracts;
using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.OpenWeather;
public class OpenWeatherWeatherRepository : AbstractWeatherRepository
{
    private OpenWeatherWeatherClient _client;
    public OpenWeatherWeatherRepository(IGeoCoder geoCoder, OpenWeatherWeatherClient client) :base(geoCoder)
    {
        _client = client;
    }

    protected override async Task<WeatherReport> GetWeatherReportFromCordinates(coordinates coordinates,Location location)
    {
        var response =  await _client.GetWeatherReport(coordinates);


        Dictionary<DateTime,Day> processed = new Dictionary<DateTime, Day>();
        DateTime currentDay = default;
        bool chanceOfPrecip = false;
        decimal countOfTemps = 0;
        decimal sumOfTemps = 0;

        foreach(var report in response.Reports)
        {
             var currentdatetime = DateTimeOffset.FromUnixTimeSeconds(report.Time);

             if(currentDay == default)
             {
                currentDay = currentdatetime.Date;
             }

             else if( currentDay != currentdatetime.Date)
             {
                processed.Add(currentDay,new Day(chanceOfPrecip,sumOfTemps/countOfTemps));
                countOfTemps = 0;
                sumOfTemps = 0;
                chanceOfPrecip = false;
                currentDay = currentdatetime.Date;
             }
             
             if(chanceOfPrecip ==false && report.ChangeOfPercipitation > 0)
             {
                chanceOfPrecip = true;
             }

             countOfTemps +=1;
             sumOfTemps += report.Main.Temperature;

        }
    
        return new WeatherReport(location,processed);
    }



}
