using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.RolePermissions.Repositories
{
    public class RolePermissionRepository : GiliX.Persistence.Repository<Domain.Models.RolePermission>, IRolePermissionRepository
    {
        protected internal RolePermissionRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
