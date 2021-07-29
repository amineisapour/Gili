using System.Threading.Tasks;

namespace AuthenticationMicroservice.Persistence.UserInformations.Repositories
{
    public interface IUserInformationRepository : GiliX.Persistence.IRepository<Domain.Models.UserInformation>
    {
        Task<Domain.Models.UserInformation> GetUserInfoByUserIdAsync(System.Guid userId);
    }
}
