using AuthenticationMicroservice.Persistence.Roles.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationMicroservice.Persistence.UserInformations.Repositories
{
    public class UserInformationRepository : GiliX.Persistence.Repository<Domain.Models.UserInformation>, IUserInformationRepository
    {
        protected internal UserInformationRepository(DbContext databaseContext) : base(databaseContext: databaseContext)
        {
        }
    }
}
