using System.Collections.Generic;

namespace Basket.API.Data.Entities
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryModel Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<CategoryModel> Children { get; set; }
    }
}
