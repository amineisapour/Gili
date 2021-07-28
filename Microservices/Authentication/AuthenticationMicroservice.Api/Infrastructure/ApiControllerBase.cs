namespace AuthenticationMicroservice.Api.Infrastructure
{
    [Microsoft.AspNetCore.Mvc.ApiController]
    [Microsoft.AspNetCore.Mvc.Route(template: "api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected Persistence.IUnitOfWork UnitOfWork { get; }
        protected Persistence.IQueryUnitOfWork QueryUnitOfWork { get; }

        public ApiControllerBase(Persistence.IUnitOfWork unitOfWork, 
            Persistence.IQueryUnitOfWork queryUnitOfWork)
        {
            UnitOfWork = unitOfWork;
            QueryUnitOfWork = queryUnitOfWork;
        }
    }
}
