namespace AuthenticationMicroservice.Persistence
{
    public interface IQueryUnitOfWork : GiliX.Persistence.IQueryUnitOfWork
    {
        public Users.Repositories.IUserQueryRepository Users { get; }
        public Roles.Repositories.IRoleQueryRepository Roles { get; }
        public Permissions.Repositories.IPermissionQueryRepository Permissions { get; }
        public UserInformations.Repositories.IUserInformationQueryRepository UserInformations { get; }
        public RolePermissions.Repositories.IRolePermissionQueryRepository RolePermissions { get; }
        public UserPermissions.Repositories.IUserPermissionQueryRepository UserPermissions { get; }
        public UserRoles.Repositories.IUserRoleQueryRepository UserRoles { get; }
        public RefreshTokens.Repositories.IRefreshTokenQueryRepository RefreshTokens { get; }
    }
}
