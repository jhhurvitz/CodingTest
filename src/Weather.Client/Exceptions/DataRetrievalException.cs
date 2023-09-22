namespace Weather.Client.Exceptions;
public class DataRetrievalException : Exception
{
    public DataRetrievalException(string msg, Exception? innerException): base($"Error Retrieve Weather Data {Environment.NewLine}{msg}",innerException) {}
}
