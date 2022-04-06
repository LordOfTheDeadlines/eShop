using Admin.API.Data.Entites;

namespace Admin.API.Data
{
    internal class CategoryMessage
    {
        public string Action { get; set; }
        public CategoryItem Category { get; set; }

        public CategoryMessage(string action, CategoryItem category)
        {
            Action = action;
            Category = category;
        }

        public static CategoryMessage CreateAdd(Category category) 
            => new CategoryMessage("AddCategory", CategoryItem.From(category));
        public static CategoryMessage CreateUpdate(Category category)
            => new CategoryMessage("UpdateCategory", CategoryItem.From(category));
        public static CategoryMessage CreateMove(Category category)
            => new CategoryMessage("MoveCategory", CategoryItem.From(category));
        public static CategoryMessage CreateDelete(Category category)
            => new CategoryMessage("DeleteCategory", CategoryItem.From(category));
    }

    public class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public static CategoryItem From(Category category)
        {
            var pId = category.ParentId==null ? 0 : category.ParentId;
            return new CategoryItem()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = pId
            };
        }
    }
}
