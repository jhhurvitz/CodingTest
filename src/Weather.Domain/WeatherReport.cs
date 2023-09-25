namespace Weather.Domain;


public record Day(bool ChanceOfPrecip, decimal temperature);

public record WeatherReport(Location Location, Dictionary<DateTime, Day> averages);