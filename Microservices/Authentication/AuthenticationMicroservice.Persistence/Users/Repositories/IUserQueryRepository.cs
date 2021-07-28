using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Persistence.Users.Repositories
{
    public interface IUserQueryRepository : GiliX.Persistence.IQueryRepository<Domain.Models.User>
    {
        Task<IList<Domain.ViewModels.UserViewModel>> GetSomeAsync(int count);
        Task<Domain.Models.User> GetUserByRefreshTokenAsync(string token);
        Task<Domain.Models.User> GetUserByUserIdAsync(System.Guid userId);
    }
}
