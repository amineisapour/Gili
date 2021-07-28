using System.Threading.Tasks;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.UserInformations.Repositories
{
    public class UserInformationQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.UserInformation>, IUserInformationQueryRepository
    {
        public UserInformationQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<UserInformation> GetUserInfoByUserIdAsync(System.Guid userId)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.User.Id == userId);
        }
    }
}
