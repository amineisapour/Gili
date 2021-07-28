namespace AuthenticationMicroservice.Persistence
{
    public class QueryUnitOfWork :
        GiliX.Persistence.QueryUnitOfWork<QueryDatabaseContext>, IQueryUnitOfWork
    {
        public QueryUnitOfWork(GiliX.Persistence.Options options) : base(options: options)
        {
        }

        private Users.Repositories.IUserQueryRepository _users;
        public Users.Repositories.IUserQueryRepository Users =>
            _users ??= new Users.Repositories.UserQueryRepository(databaseContext: DatabaseContext);

        private Roles.Repositories.IRoleQueryRepository _roles;
        public Roles.Repositories.IRoleQueryRepository Roles =>
            _roles ??= new Roles.Repositories.RoleQueryRepository(databaseContext: DatabaseContext);

        private Permissions.Repositories.IPermissionQueryRepository _permissions;
        public Permissions.Repositories.IPermissionQueryRepository Permissions =>
            _permissions ??= new Permissions.Repositories.PermissionQueryRepository(databaseContext: DatabaseContext);

        private UserInformations.Repositories.IUserInformationQueryRepository _userInformations;
        public UserInformations.Repositories.IUserInformationQueryRepository UserInformations =>
            _userInformations ??= new UserInformations.Repositories.UserInformationQueryRepository(databaseContext: DatabaseContext);

        private RolePermissions.Repositories.IRolePermissionQueryRepository _rolePermissions;
        public RolePermissions.Repositories.IRolePermissionQueryRepository RolePermissions =>
            _rolePermissions ??= new RolePermissions.Repositories.RolePermissionQueryRepository(databaseContext: DatabaseContext);

        private UserPermissions.Repositories.IUserPermissionQueryRepository _userPermissions;
        public UserPermissions.Repositories.IUserPermissionQueryRepository UserPermissions =>
            _userPermissions ??= new UserPermissions.Repositories.UserPermissionQueryRepository(databaseContext: DatabaseContext);

        private UserRoles.Repositories.IUserRoleQueryRepository _userRoles;
        public UserRoles.Repositories.IUserRoleQueryRepository UserRoles =>
            _userRoles ??= new UserRoles.Repositories.UserRoleQueryRepository(databaseContext: DatabaseContext);

        private RefreshTokens.Repositories.IRefreshTokenQueryRepository _refreshTokens;
        public RefreshTokens.Repositories.IRefreshTokenQueryRepository RefreshTokens =>
            _refreshTokens ??= new RefreshTokens.Repositories.RefreshTokenQueryRepository(databaseContext: DatabaseContext);
        
    }
}
