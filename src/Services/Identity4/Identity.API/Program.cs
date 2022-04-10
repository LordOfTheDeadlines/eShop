using System.IO;
using Identity.API.Data;
using Identity.API.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var host = CreateHostBuilder(configuration, args).Build();

            host.MigrateDatabase<ApplicationDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

                ApplicationDbContextSeed.SeedAsync(context, logger).Wait();
            })
            .MigrateDatabase<ConfigurationDbContext>((context, services) =>
            {
                ConfigurationDbContextSeed.SeedAsync(context, configuration).Wait();
            })
            .MigrateDatabase<PersistedGrantDbContext>((_, __) => { })
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //.UseKestrel(options =>
                    //{
                    //    options.Listen(IPAddress.Any, 443, listenOptions =>
                    //    {
                    //        var configuration = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));

                    //        listenOptions.UseHttps("cert.pfx", configuration["certPassword"]);
                    //    });
                    //})
                    .UseStartup<Startup>();
                });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
