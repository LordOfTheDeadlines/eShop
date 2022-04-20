using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Worker.Data.Entities
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BsonId { get; set; }
        public int UserId { get; set; }
        public List<Product> Items { get; set; } = new List<Product>();

        public Cart()
        {
        }

        public Cart(int userId)
        {
            UserId = userId;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price;
                }
                return totalprice;
            }
        }
    }
}
