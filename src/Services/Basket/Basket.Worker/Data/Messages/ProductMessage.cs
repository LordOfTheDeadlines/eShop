using Basket.Worker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Worker.Data.Messages
{
    class ProductMessage
    {
        public string Action { get; set; }
        public ProductModel Item { get; set; }
    }
}
