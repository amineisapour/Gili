namespace LoggingMicroservice.Persistence
{
	public class UnitOfWork :
        GiliX.Persistence.UnitOfWork<DatabaseContext>, IUnitOfWork
	{
		public UnitOfWork(GiliX.Persistence.Options options) : base(options: options)
		{
		}

		private Logs.Repositories.ILogRepository _logs;

		public Logs.Repositories.ILogRepository Logs
		{
			get
			{
				if (_logs == null)
				{
					_logs =
						new Logs.Repositories.LogRepository(databaseContext: DatabaseContext);
				}

				return _logs;
			}
		}
    }
}
