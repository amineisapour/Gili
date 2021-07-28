using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.UserPermissions.Repositories
{
    public class UserPermissionRepository : GiliX.Persistence.Repository<Domain.Models.UserPermission>, IUserPermissionRepository
    {
        protected internal UserPermissionRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
