﻿namespace LoggingMicroservice.Persistence.Logs.Repositories
{
	public interface ILogQueryRepository : GiliX.Persistence.IQueryRepository<Domain.Models.Log>
	{
		System.Threading.Tasks.Task
			<System.Collections.Generic.IList<ViewModels.GetLogsQueryResponseViewModel>>
			GetSomeAsync(int count);
	}
}
