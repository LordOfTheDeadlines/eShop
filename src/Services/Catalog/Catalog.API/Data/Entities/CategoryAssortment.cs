using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data.Entities
{
    public class CategoryAssortment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BsonId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public List<Category> ChildCategories { get; set; }
        public List<Product> Items { get; set; }

    }
}
