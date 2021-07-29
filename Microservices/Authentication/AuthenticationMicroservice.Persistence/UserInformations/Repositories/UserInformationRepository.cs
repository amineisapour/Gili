using System.Threading.Tasks;
using AuthenticationMicroservice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.UserInformations.Repositories
{
    public class UserInformationRepository : GiliX.Persistence.Repository<Domain.Models.UserInformation>, IUserInformationRepository
    {
        protected internal UserInformationRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }

        public async Task<UserInformation> GetUserInfoByUserIdAsync(System.Guid userId)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.User.Id == userId);
        }
    }
}
