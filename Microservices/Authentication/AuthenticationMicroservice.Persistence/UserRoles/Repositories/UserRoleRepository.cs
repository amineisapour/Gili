using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.UserRoles.Repositories
{
    public class UserRoleRepository : GiliX.Persistence.Repository<Domain.Models.UserRole>, IUserRoleRepository
    {
        protected internal UserRoleRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
