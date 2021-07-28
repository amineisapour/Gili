using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.Users.Repositories
{
    public class UserQueryRepository : GiliX.Persistence.QueryRepository<Domain.Models.User>, IUserQueryRepository
    {
        public UserQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IList<Domain.ViewModels.UserViewModel>> GetSomeAsync(int count)
        {
            // Note: ToListAsync -> Extension Method -> using Microsoft.EntityFrameworkCore;
            var result =
                    await
                        DbSet
                            .OrderByDescending(current => current.RegisterDateTime)
                            .Skip(count: 0)
                            .Take(count: count)
                            .Select(current => new Domain.ViewModels.UserViewModel()
                            {
                                Id = current.Id,
                                Username = current.Username,
                                RegisterDateTime = current.RegisterDateTime,
                                IsActive = current.IsActive,
                                PasswordHash = current.PasswordHash,
                                PasswordSalt = current.PasswordSalt
                            })
                            .ToListAsync()
                ;

            return result;
        }

        public async Task<Domain.Models.User> GetUserByRefreshTokenAsync(string token)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        }

        public async Task<Domain.Models.User> GetUserByUserIdAsync(System.Guid userId)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}
