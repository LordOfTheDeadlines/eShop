using Catalog.Worker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Worker.Data.Messages
{
    class CategoryMessage
    {
        public string Action { get; set; }
        public CategoryModel Category { get; set; }
    }
}
