using Microsoft.EntityFrameworkCore;
using WebApplicationPermissions.Models;

namespace WebApplicationPermissions.Context
{
    public class DefaultContext: DbContext
    {
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany()
                .HasForeignKey(p => p.TipoPermiso);
        }
    }
}
