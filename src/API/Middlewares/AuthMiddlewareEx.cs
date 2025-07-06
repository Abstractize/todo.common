using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using API.Common.Middlewares.Auth;

namespace API.Common.Middlewares
{
    public static class AuthMiddlewareEx
    {
        public static void AddAuthConfiguration(this IServiceCollection services, string jwtIssuer, string jwtAudience, string jwtKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddAuthConfiguration(jwtIssuer, jwtAudience, jwtKey);

            services.AddAuthorization(options => options.AddPolicies());
        }

        public static void UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
