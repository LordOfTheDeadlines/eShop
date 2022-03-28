using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Admin.API.Data.Entites
{
    [Table("Products")]
    public class Product
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

    }
}
