using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.Users.Repositories
{
    public class UserRepository : GiliX.Persistence.Repository<Domain.Models.User>, IUserRepository
    {
        protected internal UserRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }

        public async Task<Domain.Models.User> GetByUsernameAsync(string username)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
