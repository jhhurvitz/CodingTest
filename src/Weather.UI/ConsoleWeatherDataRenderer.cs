using System.Text;
using Weather.UI.Abstracts;
using Weather.Client.Abstracts;
using Weather.Client.Exceptions;

namespace Weather.UI;

/// <summary>
/// Class Responsible for Rending out to Console
/// </summary>
public class ConsoleWeatherDataRenderer : IWeatherDataRenderer
{
    private readonly IWeatherRepository _weatherRepository;
    private  HashSet<Location> _locations = new HashSet<Location>(); // record types equality is determined by value of each property not reference so hash set will make sure no dupes are presnent. 

    /// <summary>
    /// Constructor for UI Renderer
    /// </summary>
    /// <param name="weatherRepository">any IWeather Repository</param>
    public ConsoleWeatherDataRenderer(IWeatherRepository weatherRepository) => _weatherRepository = weatherRepository;

    public void AddLUpdateLocation(Location location)
    {
        _locations.Add(location);
    }


    /// <summary>
    /// Add Location to Render
    /// </summary>
    /// <param name="locations">List of Location Records</param>
    public void AddUpdateLocations(IEnumerable<Location> locations)
    {
        foreach(var location in locations)
        {
            AddLUpdateLocation(location);
        }
    }
    
    /// <summary>
    /// Retireves the Weather Reports in Parallel then Outputs them in the correct format
    /// </summary>
    /// <returns>Task</returns>
    public async Task Render()
    {
  

        var locationOutputs = await Task.WhenAll(_locations.Select(location=>RenderLocation(location)));

        foreach(var locationOutput in locationOutputs)
        {
            Console.Write(locationOutput);
        }
    }


/// <summary>
/// Task that Renders an indvidual Location 
/// </summary>
/// <param name="location"></param>
/// <returns>Returns the final string to be rendered</returns>
    private async Task<string> RenderLocation(Location location)
    {    StringBuilder output = new StringBuilder();
        output.AppendLine(new string('_', 30));
        output.AppendLine($"{location.City}, {location.State} ({location.ZipCode})");
        output.AppendLine();
        output.AppendLine("Date       Avg Temp(F)");
        output.AppendLine(new string('-', 30));

        try
        {
            var weather = await _weatherRepository.GetWeatherReport(location);
            var today = DateTime.Today;
            foreach(var average in weather.averages.Where(report=> report.Key.Date != today).Take(5)) // only want 5 next days not today seperating concerns could do that at the service layer but UI concern makes it easier to change if requirements change or re-use of service layer
            {
                string temperatureLine = $"{average.Key.Date:mm/dd/yyyy}{(average.Value.ChanceOfPrecip ? "* ": "  ")}{average.Value.temperature} F";
                output.AppendLine(temperatureLine);
            }
            output.AppendLine();
            output.AppendLine();
           
        }
        catch(DataRetrievalException exception)
        {
            output.AppendLine($"an error has occured retrieving your data {exception.Message}");
        }

         return output.ToString();
    }
}
