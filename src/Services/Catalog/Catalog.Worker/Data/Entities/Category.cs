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
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Category From(CategoryModel category)
        {
            return new Category()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

    }
}
