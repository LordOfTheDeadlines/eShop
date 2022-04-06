using Admin.API.Data.Entites;

namespace Admin.API.Data
{
    public class ProductMessage
    {
        public string Action { get; set; }
        public ProductItem Item { get; set; }

        public ProductMessage(string action, ProductItem item)
        {
            Action = action;
            Item = item;
        }

        public static ProductMessage CreateAdd(Product item)
            => new ProductMessage("AddItem", ProductItem.From(item));
        public static ProductMessage CreateUpdate(Product item)
            => new ProductMessage("UpdateItem", ProductItem.From(item));
        public static ProductMessage CreateDelete(Product item)
            => new ProductMessage("DeleteItem", ProductItem.From(item));
    }

    public class ProductItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        public static ProductItem From(Product item)
        {
            return new ProductItem()
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
