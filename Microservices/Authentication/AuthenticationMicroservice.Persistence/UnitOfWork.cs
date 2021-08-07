using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence
{
    public class UnitOfWork :
        GiliX.Persistence.UnitOfWork<DatabaseContext>, IUnitOfWork
    {
        public UnitOfWork(GiliX.Persistence.Options options) : base(options: options)
        {
        }

        public DatabaseContext Contex => DatabaseContext;

        private Users.Repositories.IUserRepository _users;
        public Users.Repositories.IUserRepository Users =>
            _users ??= new Users.Repositories.UserRepository(databaseContext: DatabaseContext);

        private Roles.Repositories.IRoleRepository _roles;
        public Roles.Repositories.IRoleRepository Roles =>
            _roles ??= new Roles.Repositories.RoleRepository(databaseContext: DatabaseContext);

        private Permissions.Repositories.IPermissionRepository _permissions;
        public Permissions.Repositories.IPermissionRepository Permissions =>
            _permissions ??= new Permissions.Repositories.PermissionRepository(databaseContext: DatabaseContext);

        private UserInformations.Repositories.IUserInformationRepository _userInformations;
        public UserInformations.Repositories.IUserInformationRepository UserInformations =>
            _userInformations ??= new UserInformations.Repositories.UserInformationRepository(databaseContext: DatabaseContext);

        private RolePermissions.Repositories.IRolePermissionRepository _rolePermissions;
        public RolePermissions.Repositories.IRolePermissionRepository RolePermissions =>
            _rolePermissions ??= new RolePermissions.Repositories.RolePermissionRepository(databaseContext: DatabaseContext);

        private UserPermissions.Repositories.IUserPermissionRepository _userPermissions;
        public UserPermissions.Repositories.IUserPermissionRepository UserPermissions =>
            _userPermissions ??= new UserPermissions.Repositories.UserPermissionRepository(databaseContext: DatabaseContext);

        private UserRoles.Repositories.IUserRoleRepository _userRoles;
        public UserRoles.Repositories.IUserRoleRepository UserRoles =>
            _userRoles ??= new UserRoles.Repositories.UserRoleRepository(databaseContext: DatabaseContext);

        private RefreshTokens.Repositories.IRefreshTokenRepository _refreshTokens;
        public RefreshTokens.Repositories.IRefreshTokenRepository RefreshTokens =>
            _refreshTokens ??= new RefreshTokens.Repositories.RefreshTokenRepository(databaseContext: DatabaseContext);
    }
}
