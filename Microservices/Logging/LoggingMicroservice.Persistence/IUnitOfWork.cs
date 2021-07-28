namespace LoggingMicroservice.Persistence
{
	public interface IUnitOfWork : GiliX.Persistence.IUnitOfWork
	{
		public Logs.Repositories.ILogRepository Logs { get; }
	}
}
