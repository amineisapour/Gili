namespace AuthenticationMicroservice.Persistence.UserRoles.Repositories
{
    public class UserRoleQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.UserRole>, IUserRoleQueryRepository
    {
        public UserRoleQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
