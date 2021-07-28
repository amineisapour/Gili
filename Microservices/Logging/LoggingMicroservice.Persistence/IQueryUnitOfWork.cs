namespace LoggingMicroservice.Persistence
{
    public interface IQueryUnitOfWork : GiliX.Persistence.IQueryUnitOfWork
    {
        public Logs.Repositories.ILogQueryRepository Logs { get; }
    }
}
