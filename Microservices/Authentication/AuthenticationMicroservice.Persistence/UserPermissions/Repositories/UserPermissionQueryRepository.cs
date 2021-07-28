namespace AuthenticationMicroservice.Persistence.UserPermissions.Repositories
{
    public class UserPermissionQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.UserPermission>, IUserPermissionQueryRepository
    {
        public UserPermissionQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
