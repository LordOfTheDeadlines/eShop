using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;

namespace AdminWebApp.Extensions
{
    public static class CustomAuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callbackUrl = configuration.GetValue<string>("CallBackUrl");
            var clientId = configuration.GetValue<string>("ClientId");
            var clientSecret = configuration.GetValue<string>("ClientSecret");

            var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //opts.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opts =>
            {
                opts.Authority = identityUrl;

                opts.ClientId = clientId;
                opts.ClientSecret = clientSecret;

                opts.SignedOutRedirectUri = callbackUrl;
                // hybrid
                opts.ResponseType = "code id_token";
                opts.SaveTokens = true;
                opts.GetClaimsFromUserInfoEndpoint = true;

                opts.RequireHttpsMetadata = false;
                opts.Scope.Add("AdminWebApp");
            });

            return services;
        }
    }
}
