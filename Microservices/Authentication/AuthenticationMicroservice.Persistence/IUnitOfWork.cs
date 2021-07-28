namespace AuthenticationMicroservice.Persistence
{
	public interface IUnitOfWork : GiliX.Persistence.IUnitOfWork
	{
		public Users.Repositories.IUserRepository Users { get; }
        public Roles.Repositories.IRoleRepository Roles { get; }
        public Permissions.Repositories.IPermissionRepository Permissions { get; }
        public UserInformations.Repositories.IUserInformationRepository UserInformations { get; }
        public RolePermissions.Repositories.IRolePermissionRepository RolePermissions { get; }
        public UserPermissions.Repositories.IUserPermissionRepository UserPermissions { get; }
        public UserRoles.Repositories.IUserRoleRepository UserRoles { get; }
        public RefreshTokens.Repositories.IRefreshTokenRepository RefreshTokens { get; }

    }
}
