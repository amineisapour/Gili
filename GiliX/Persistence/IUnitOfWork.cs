namespace GiliX.Persistence
{
	public interface IUnitOfWork : IQueryUnitOfWork
	{
		System.Threading.Tasks.Task SaveAsync();
	}
}
