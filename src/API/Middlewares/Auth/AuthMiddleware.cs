using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Common.Middlewares.Auth
{
    internal static class AuthEx
    {
        public static void AddAuthConfiguration(this AuthenticationBuilder builder, string jwtIssuer, string jwtAudience, string jwtKey)
        {
            builder.AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey))
                    };

                    opts.SaveToken = true;
                }
            );
        }

        public static void AddPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy("AllowAnonymous", policy => policy.RequireAssertion(context => true));
            options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
        }
    }
}
