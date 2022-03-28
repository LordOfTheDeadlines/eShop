using Admin.API.Data.Entites;

namespace Admin.API.Data
{
    internal class CategoryMessage
    {
        public string Action { get; set; }
        public SimpleCategory Category { get; set; }

        public CategoryMessage(string action, SimpleCategory category)
        {
            Action = action;
            Category = category;
        }

        public static CategoryMessage CreateAdd(Category category) 
            => new CategoryMessage("AddCategory", SimpleCategory.From(category));
        public static CategoryMessage CreateUpdate(Category category)
            => new CategoryMessage("UpdateCategory", SimpleCategory.From(category));
        public static CategoryMessage CreateMove(Category category)
            => new CategoryMessage("MoveCategory", SimpleCategory.From(category));
        public static CategoryMessage CreateDelete(Category category)
            => new CategoryMessage("DeleteCategory", SimpleCategory.From(category));
    }

    public class SimpleCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public static SimpleCategory From(Category category)
        {
            return new SimpleCategory()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.Parent.Id
            };
        }
    }
}
