using OcelotApiGw.Data;
using OcelotApiGw.Repositories;
using OcelotApiGw.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OcelotApiGw.Extensions
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
