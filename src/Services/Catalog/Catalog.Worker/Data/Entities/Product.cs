using Catalog.Worker.Data.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Worker.Data.Entities
{
    [BsonNoId]
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }

        public static Product From(ProductModel item)
        {
            return new Product()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageUrl = item.ImageUrl
            };
        }

    }
}
