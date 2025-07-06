using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Models.Common.Exceptions;

namespace API.Common.Middlewares.Exceptions;

internal class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    private static readonly JsonSerializerOptions _camelCasejsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (WebAppException ex)
        {
            await WriteErrorResponse(context, ex);
        }
        catch (Exception ex)
        {
            WebAppException webEx = ex switch
            {
                UnauthorizedAccessException => new UnauthorizedException(),
                ArgumentNullException => new BadRequestException("Missing argument"),
                ArgumentException => new BadRequestException("Invalid argument"),
                KeyNotFoundException => new NotFoundException(ex.Message),
                _ => new InternalServerErrorException()
            };

            await WriteErrorResponse(context, webEx);
        }
    }

    private static Task WriteErrorResponse(HttpContext context, WebAppException ex)
    {
        context.Response.StatusCode = ex.StatusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = ex.Message,
            status = ex.StatusCode
        };

        string responseJson = JsonSerializer.Serialize(response, _camelCasejsonOptions);
        return context.Response.WriteAsync(responseJson);
    }
}
