using Admin.API.Data.Entites;

namespace Admin.API.Data
{
    public class BasketMessage
    {
        public string Action { get; set; }
        public ProductItem Item { get; set; }

        public BasketMessage(string action, ProductItem item)
        {
            Action = action;
            Item = item;
        }

        public static BasketMessage CreateUpdate(Product item)
            => new BasketMessage("UpdateItem", ProductItem.From(item));
        public static BasketMessage CreateDelete(Product item)
            => new BasketMessage("DeleteItem", ProductItem.From(item));
    }
}
