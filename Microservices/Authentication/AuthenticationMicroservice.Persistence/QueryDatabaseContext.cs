using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence
{
	public class QueryDatabaseContext : DbContext
	{
		public QueryDatabaseContext(DbContextOptions<QueryDatabaseContext> options) : base(options: options)
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

		protected override void OnModelCreating
			(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
