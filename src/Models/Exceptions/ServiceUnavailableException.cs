namespace Models.Common.Exceptions;

public sealed class ServiceUnavailableException(string message) : WebAppException(message, 503)
{
    public ServiceUnavailableException() : this("The service is currently unavailable")
    { }
}



