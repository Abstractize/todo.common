namespace Models.Common.Exceptions;

public sealed class InternalServerErrorException(string message) : WebAppException(message, 500)
{
    public InternalServerErrorException() : this("An internal server error occurred")
    { }
}



