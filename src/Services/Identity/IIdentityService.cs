namespace Services.Common.Identity;

public interface IIdentityService
{
    Guid? UserId { get; }
    string? Email { get; }
    string? Roles { get; }
    string? Name { get; }
    bool IsAuthenticated { get; }
}
