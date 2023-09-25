using Microsoft.Extensions.Logging;
using Weather.Client.Abstracts;
using Weather.Client.Models;
using Weather.Domain;

namespace Weather.Client.OpenWeather;
public class OpenWeatherWeatherRepository : AbstractWeatherRepository
{
    private OpenWeatherWeatherClient _client;
    private readonly ILogger<OpenWeatherWeatherRepository> _logger;

    public OpenWeatherWeatherRepository(IGeoCoder geoCoder, OpenWeatherWeatherClient client,ILogger<OpenWeatherWeatherRepository> logger) : base(geoCoder)
    {
        _client = client;
        _logger = logger;
    }

    protected override async Task<WeatherReport> GetWeatherReportFromCoordinates(Coordinates coordinates, Location location)
    {
     var response = await _client.GetWeatherReport(coordinates);


        Dictionary<DateTime, Day> processed = new Dictionary<DateTime, Day>();
        DateTime currentDay = default;
        bool chanceOfPrecip = false;
        int countOfTemps = 0;
        decimal sumOfTemps = 0;

        Console.WriteLine(response.Count);

        using var enumerator = response.Reports.GetEnumerator();
        var last = !enumerator.MoveNext();

        while (!last)
        {
            var current = enumerator.Current;
            var currentdatetime = DateTimeOffset.FromUnixTimeSeconds(current.Time); // this will be in local time relative to user
            last = !enumerator.MoveNext();

            if (currentDay == default || currentDay != currentdatetime.Date)
            {
                if (countOfTemps > 0)
                {
                    processed.Add(currentDay, new Day(chanceOfPrecip, sumOfTemps / countOfTemps));
                }

                countOfTemps = 0;
                sumOfTemps = 0;
                chanceOfPrecip = false;
                currentDay = currentdatetime.Date;
            }

            if (!chanceOfPrecip && current.ChangeOfPercipitation > 0)
            {
                chanceOfPrecip = true;
            }

            _logger.LogDebug($"CurrentDateTime : {currentdatetime}  {current.Main.Temperature}");
            countOfTemps++;
            sumOfTemps += current.Main.Temperature;
        }

        if (countOfTemps > 0)
        {
            processed.Add(currentDay, new Day(chanceOfPrecip, sumOfTemps / countOfTemps));
        }


        return new WeatherReport(location, processed);


    }



}
