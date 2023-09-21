namespace Weather.Domain;

public record WeatherReport(Location Location, decimal FiveDayAverage, decimal ChanceOfPrecipitation);