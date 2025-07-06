using API.Common.Middlewares.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace API.Common.Middlewares;

public static class JsonExtensions
{
    public static IServiceCollection AddCamelCaseJson(this IServiceCollection services)
        => services.Configure<JsonOptions>(opt => opt.UseCamelCaseJson());

    public static IMvcBuilder AddCamelCaseJson(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(opt => opt.UseCamelCaseJson());
        return builder;
    }
}