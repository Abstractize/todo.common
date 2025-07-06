using API.Common.Middlewares.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace API.Common.Middlewares;

public static class ExceptionHandlingMiddlewareEx
{
    public static IApplicationBuilder UseJsonExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}