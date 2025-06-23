using LoginSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Data
{
    public class SSODbContext : DbContext
    {
        public SSODbContext(DbContextOptions<SSODbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => rp.Id);

            
        }
    }

}
