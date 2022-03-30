using Catalog.Worker.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Worker.Data.Entities
{
    class CategoryAssortment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BsonId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public List<Category> ChildCategories { get; set; }
        public List<Product> Items { get; set; }

        public static CategoryAssortment Root()
        {
            return new CategoryAssortment()
            {
                Id = 0,
                Name = "",
                ChildCategories = new List<Category>(),
                Items = new List<Product>()
            };
        }

        public static CategoryAssortment From(CategoryModel category)
        {
            return new CategoryAssortment()
            {
                Id = category.Id,
                Name = category.Name,
                ChildCategories = new List<Category>(),
                Items = new List<Product>(),
                Parent = category.ParentId
            };
        }

    }
}
