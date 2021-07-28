using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.Permissions.Repositories
{
    public class PermissionRepository : GiliX.Persistence.Repository<Domain.Models.Permission>, IPermissionRepository
    {
        protected internal PermissionRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
