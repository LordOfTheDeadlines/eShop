using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models
{
    public class CategoryAssortment
    {
        public string BsonId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public IEnumerable<Category> ChildCategories { get; set; }
        public IEnumerable<Product> Items { get; set; }
    }
}
