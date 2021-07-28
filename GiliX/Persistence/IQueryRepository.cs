namespace GiliX.Persistence
{
	public interface IQueryRepository<T> where T : Domain.IEntity
	{
		System.Threading.Tasks.Task<T> GetByIdAsync(System.Guid id);

		System.Threading.Tasks.Task<System.Collections.Generic.IList<T>> GetAllAsync();

	}
}
