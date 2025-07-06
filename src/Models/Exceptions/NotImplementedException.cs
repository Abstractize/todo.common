namespace Models.Common.Exceptions;

public sealed class NotImplementedException(string message) : WebAppException(message, 501)
{
    public NotImplementedException() : this("This feature is not implemented yet")
    { }
}



