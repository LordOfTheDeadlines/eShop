using Auth.API.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Auth.API.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication().AddJwtBearer("IdentityApiKey", options =>
            {
                var jwtConfig = new AuthConfiguration();
                config.GetSection("Authentication").Bind(jwtConfig);
                var validationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.AccessTokenSecret)),
                    ValidAudience = "usersAudience",
                    ValidIssuer = "usersIssuer",
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.TokenValidationParameters = validationParameters;
            });
            return services;
        }
    }
}
