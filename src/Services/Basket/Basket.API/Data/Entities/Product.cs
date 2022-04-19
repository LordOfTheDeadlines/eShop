using Basket.API.Data.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    [BsonNoId]
    public class Product
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
                Price = (int)item.Price,
                ImageUrl = item.ImageUrl
            };
        }
    }
}
