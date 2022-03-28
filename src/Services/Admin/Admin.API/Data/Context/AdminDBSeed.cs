using Admin.API.Data.Entites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Data.Context
{
    public class AdminDBSeed
    {
        public static async Task SeedAsync(AdminDB context, ILogger<AdminDBSeed> logger)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(GetPreconfiguredCategories());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database categories associated with context {DbContextName}", typeof(AdminDB).Name);
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(GetPreconfiguredProducts(context));
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database products associated with context {DbContextName}", typeof(AdminDB).Name);
            }
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>
            {
                new Category() { Name = "Books"},
                new Category() { Name="Notebooks"},
                new Category() { Name="Fantasy", ParentId=1},
            };
        }
        private static IEnumerable<Product> GetPreconfiguredProducts(AdminDB context)
        {
            return new List<Product>
            {
                new Product() { Name="Lord of the rings: the fellowship of the ring book", CategoryId=3, Price=100.00, ImageUrl="https://i.pinimg.com/originals/ae/30/ec/ae30ec9d01b42f30cb041ce3a9ec96f5.jpg" },
                new Product() { Name="Lord of the rings: two towers", CategoryId=3, Price=150.00, ImageUrl="https://i.pinimg.com/originals/ae/30/ec/ae30ec9d01b42f30cb041ce3a9ec96f5.jpg" }
            };
        }
    }
}
