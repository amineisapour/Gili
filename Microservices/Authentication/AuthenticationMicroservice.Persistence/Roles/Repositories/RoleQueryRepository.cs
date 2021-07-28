namespace AuthenticationMicroservice.Persistence.Roles.Repositories
{
    public class RoleQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.Role>, IRoleQueryRepository
    {
        public RoleQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
