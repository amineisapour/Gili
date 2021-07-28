namespace AuthenticationMicroservice.Persistence.Permissions.Repositories
{
    public class PermissionQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.Permission>, IPermissionQueryRepository
    {
        public PermissionQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
