namespace Models.Common.Exceptions;

public sealed class BadRequestException(string message) : WebAppException(message, 400)
{
    public BadRequestException(string field, string message) : this($"{field} is invalid: {message}")
    { }
}



