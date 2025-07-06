namespace Models.Common.Exceptions;

public sealed class UnauthorizedException(string message) : WebAppException(message, 401)
{
    public UnauthorizedException() : this("You are not authorized to perform this action")
    { }
}



