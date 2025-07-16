namespace Models.Common.Exceptions;

public sealed class ForbiddenException(string message) : WebAppException(message, 403)
{
    public ForbiddenException() : this("You do not have permission to access this resource")
    { }
}
