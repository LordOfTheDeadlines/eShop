using Admin.API.Data.Context;
using Admin.API.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Admin.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
               .Build()
               .MigrateDatabase<AdminDB>((context, services) =>
               {
                   var logger = services.GetService<ILogger<AdminDBSeed>>();
                   //AdminDBSeed
                   //    .SeedAsync(context, logger)
                   //    .Wait();
               })
               .Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
