using Data.Common.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data.Common.Extensions;

public static class HostExtensions
{
    public static void ApplyMigrations<TDatabaseContext>(this IHost host) where TDatabaseContext : BaseContext<TDatabaseContext>
    {
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDatabaseContext>();
        dbContext.Database.Migrate();
    }
}