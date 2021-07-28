using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options: options)
        {
            // TODO
            Database.EnsureCreated();
        }

        public DbSet<Domain.Models.User> Users { get; set; }
        public DbSet<Domain.Models.Role> Roles { get; set; }
        public DbSet<Domain.Models.Permission> Permissions { get; set; }
        public DbSet<Domain.Models.UserInformation> UserInformations { get; set; }
        public DbSet<Domain.Models.RolePermission> RolePermissions { get; set; }
        public DbSet<Domain.Models.UserPermission> UserPermissions { get; set; }
        public DbSet<Domain.Models.UserRole> UserRoles { get; set; }
        public DbSet<Domain.Models.RefreshToken> RefreshTokens { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new Configs.UserConfig());
            //modelBuilder.ApplyConfiguration(new Configs.PermissionConfig());
            //modelBuilder.ApplyConfiguration(new Configs.UserInformationConfig());
            //modelBuilder.ApplyConfiguration(new Configs.RoleConfig());

            //modelBuilder.Entity<Domain.Models.User>()
            //    .HasOne(m => m.UserInformation)
            //    .WithOne(i => i.User)
            //    .HasForeignKey<Domain.Models.UserInformation>(b => b.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
