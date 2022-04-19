using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<Product> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
