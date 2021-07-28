namespace AuthenticationMicroservice.Persistence.RefreshTokens.Repositories
{
    public class RefreshTokenQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.RefreshToken>, IRefreshTokenQueryRepository
    {
        public RefreshTokenQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
