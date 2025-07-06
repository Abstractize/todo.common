namespace Models.Common.Exceptions;

public abstract class WebAppException : Exception
{
    public int StatusCode { get; set; }

    protected WebAppException(string message, int statusCode = 500) : base(message)
        => StatusCode = statusCode;
}



