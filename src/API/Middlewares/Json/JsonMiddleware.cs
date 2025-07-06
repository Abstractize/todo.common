using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;

namespace API.Common.Middlewares.Json;

internal static class JsonMiddleware
{
    public static void UseCamelCaseJson(this JsonOptions opt)
    {
        opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    }

    public static void UseCamelCaseJson(this Microsoft.AspNetCore.Mvc.JsonOptions opt)
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    }
}
