using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.Roles.Repositories
{
    public class RoleRepository : GiliX.Persistence.Repository<Domain.Models.Role>, IRoleRepository
    {
        protected internal RoleRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
