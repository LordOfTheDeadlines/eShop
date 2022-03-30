using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Catalog.API.Data.Entities
{
    [BsonNoId]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }
    }
}
