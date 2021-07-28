using System.Threading.Tasks;

namespace AuthenticationMicroservice.Persistence.Users.Repositories
{
    public interface IUserRepository : GiliX.Persistence.IRepository<Domain.Models.User>
    {
        Task<Domain.Models.User> GetByUsernameAsync(string username);
    }
}
