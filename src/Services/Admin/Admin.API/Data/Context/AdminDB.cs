using Admin.API.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace Admin.API.Data.Context
{
    public class AdminDB:DbContext
    {
        public AdminDB(DbContextOptions<AdminDB> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
}
}
