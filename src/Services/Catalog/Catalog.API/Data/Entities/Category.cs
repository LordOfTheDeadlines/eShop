using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data.Entities
{
    [BsonNoId]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
