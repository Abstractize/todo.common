using Microsoft.Extensions.DependencyInjection;

namespace Services.Common.Identity;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, Implementation.IdentityService>();
        return services;
    }
}