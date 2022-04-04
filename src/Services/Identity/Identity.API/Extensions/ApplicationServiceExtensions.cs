using Identity.API.Data;
using Identity.API.Repositories;
using Identity.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AuthContext>(opts =>
            {
                opts.UseSqlServer(config.GetConnectionString("AuthConnectionString"));
            });

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }
    }
}
