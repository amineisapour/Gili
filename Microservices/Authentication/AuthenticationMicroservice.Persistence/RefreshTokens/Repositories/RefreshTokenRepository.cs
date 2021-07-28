using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.RefreshTokens.Repositories
{
    public class RefreshTokenRepository : GiliX.Persistence.Repository<Domain.Models.RefreshToken>, IRefreshTokenRepository
    {
        protected internal RefreshTokenRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
