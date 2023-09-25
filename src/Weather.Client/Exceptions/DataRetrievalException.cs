namespace Weather.Client.Exceptions;

/// <summary>
/// exception that represents an error while retireving weather data. 
/// </summary>
public class DataRetrievalException : Exception
{
    public DataRetrievalException(string msg, Exception innerException) : base($"Error Retrieve Weather Data {Environment.NewLine}{msg}", innerException) { }
}
