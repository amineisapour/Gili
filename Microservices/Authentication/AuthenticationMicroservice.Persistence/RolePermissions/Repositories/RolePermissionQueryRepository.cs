namespace AuthenticationMicroservice.Persistence.RolePermissions.Repositories
{
    public class RolePermissionQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.RolePermission>, IRolePermissionQueryRepository
    {
        public RolePermissionQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
