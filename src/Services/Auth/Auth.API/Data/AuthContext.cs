using Auth.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Data
{
    public class AuthContext : DbContext
    {

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }

        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasMany(u => u.Roles).WithOne(r => r.User);
        }
    }
}
