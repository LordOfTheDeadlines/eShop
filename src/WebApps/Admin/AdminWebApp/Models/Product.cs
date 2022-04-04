using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Models
{
    public class Product
    {
        public Product() { }

        public Product(int id, string name, double price, string imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            CategoryId = null;
            Category = null;
        }

        public Product(string name, int? categoryId, double price, string imageUrl)
        {
            Name = name;
            CategoryId = categoryId;
            Price = price;
            ImageUrl = imageUrl;
        }

        public Product(int id, string name, int? categoryId, double price, string imageUrl)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
            ImageUrl = imageUrl;
        }

        public Product(int id, string name, double price, string imageUrl, Category category, int? categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Category = category;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

    }
}
