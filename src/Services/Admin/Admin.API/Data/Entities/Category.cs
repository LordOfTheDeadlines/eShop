using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Admin.API.Data.Entites
{
    [Table("Categories")]
    public class Category
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public Category Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<Category> Children { get; set; }

    }
}
