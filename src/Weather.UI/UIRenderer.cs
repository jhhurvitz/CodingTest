using System.Text;
using Weather.Client.Abstracts;
using Weather.Client.Exceptions;

namespace Weather.UI;
public class UIRenderer
{
    private readonly IWeatherRepository _weatherRepository;
    private  IEnumerable<Location> _locations = new List<Location>();
    public UIRenderer(IWeatherRepository weatherRepository) => _weatherRepository = weatherRepository;



    public void AddLocations(IEnumerable<Location> locations)
    {
        _locations = locations;
    }
    
    public async Task Render()
    {
  

        var locationOutputs = await Task.WhenAll(_locations.Select(location=>RenderLocation(location)));

        foreach(var locationOutput in locationOutputs)
        {
            Console.Write(locationOutput);
        }
    }

    private async Task<string> RenderLocation(Location location)
    {    StringBuilder output = new StringBuilder();
        output.AppendLine(new string('_', 30));
        output.AppendLine($"{location.City}, {location.State}");
        output.AppendLine();
        output.AppendLine("Date       Avg Temp(F)");
        output.AppendLine(new string('-', 30));

        try
        {
            var weather = await _weatherRepository.GetWeatherReport(location);
     
            foreach(var average in weather.averages)
            {
                string temperatureLine = $"{average.Key.Date:mm/dd/yyyy}{(average.Value.ChanceOfPrecip ? "* ": "  ")}{average.Value.temperature:F2} F";
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
