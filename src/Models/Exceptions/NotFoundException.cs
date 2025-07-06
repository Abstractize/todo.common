namespace Models.Common.Exceptions;

public sealed class NotFoundException(string message) : WebAppException(message, 404)
{
    public NotFoundException(Guid id) : this($"Item with id: {id} was not found")
    { }
}



