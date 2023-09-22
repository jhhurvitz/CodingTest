using System.Text.Json.Serialization;


namespace Weather.Client.OpenWeather.Models;
public class WeatherResponse
{
    [JsonPropertyName("cnt")]
    public decimal Count {get;set;}

    [JsonPropertyName("list")]
    public List<Report> Reports {get;set;}
}

public class Report 
{
    [JsonPropertyName("dt")]
    public long Time {get;set;}

      [JsonPropertyName("dt_txt")]
    public string DateTime {get;set;}
    public ReportMain Main {get;set;}
   
    [JsonPropertyName("pop")]
    public decimal ChangeOfPercipitation {get; set;}
}

public class ReportMain
{


    [JsonPropertyName("temp")]
    public decimal Temperature {get;set;}
}