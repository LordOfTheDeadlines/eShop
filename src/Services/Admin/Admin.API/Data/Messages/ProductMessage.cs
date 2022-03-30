using Admin.API.Data.Entites;

namespace Admin.API.Data
{
    public class ProductMessage
    {
        public string Action { get; set; }
        public SimpleItem Item { get; set; }

        public ProductMessage(string action, SimpleItem item)
        {
            Action = action;
            Item = item;
        }

        public static ProductMessage CreateAdd(Product item)
            => new ProductMessage("AddItem", SimpleItem.From(item));
        public static ProductMessage CreateUpdate(Product item)
            => new ProductMessage("UpdateItem", SimpleItem.From(item));
        public static ProductMessage CreateDelete(Product item)
            => new ProductMessage("DeleteItem", SimpleItem.From(item));
    }

    public class SimpleItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        public static SimpleItem From(Product item)
        {
            return new SimpleItem()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                CategoryId = (int)item.CategoryId
            };
        }
    }
}
