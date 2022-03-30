using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Models
{
    public class Category
    {

        public Category() { }
        public Category(string name, int? parentId)
        {
            Name = name;
            ParentId = parentId;
        }

        public Category(string name, Category parent, int id)
        {
            Name = name;
            Parent = parent;
            ParentId = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Category Parent { get; set; }
        public int? ParentId { get; set; }
    }
}
