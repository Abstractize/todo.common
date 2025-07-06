using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services.Common.Identity.Implementation;

internal class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal? _user;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _user = _httpContextAccessor.HttpContext?.User;
    }

    public Guid? UserId
    {
        get
        {
            Claim? userIdClaim = _user?.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim == null ?
                null : Guid.TryParse(userIdClaim.Value, out var userId) ?
                userId : null;
        }
    }

    public string? Email =>
        _user?.FindFirst(ClaimTypes.Email)?.Value;

    public string? Roles =>
        _user?.FindFirst(ClaimTypes.Role)?.Value;

    public string? Name =>
        _user?.FindFirst(ClaimTypes.Name)?.Value;

    public bool IsAuthenticated =>
        _user?.Identity?.IsAuthenticated ?? false;
}