using System.Threading.Tasks;

namespace AuthenticationMicroservice.Persistence.UserInformations.Repositories
{
    public interface IUserInformationQueryRepository : GiliX.Persistence.IQueryRepository<Domain.Models.UserInformation>
    {
        Task<Domain.Models.UserInformation> GetUserInfoByUserIdAsync(System.Guid userId);
    }
}
