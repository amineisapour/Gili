﻿namespace LoggingMicroservice.Application.Logs.CommandHandlers
{
	public class GetLogsQueryHandler : object,
        GiliX.Mediator.IRequestHandler
		<Queries.GetLogsQuery, System.Collections.Generic.IEnumerable<Persistence.ViewModels.GetLogsQueryResponseViewModel>>
	{
		public GetLogsQueryHandler
			(GiliX.Logging.ILogger<CreateLogCommandHandler> logger,
			AutoMapper.IMapper mapper,
			Persistence.IQueryUnitOfWork unitOfWork) : base()
		{
			Logger = logger;
			Mapper = mapper;
			UnitOfWork = unitOfWork;
		}

		protected AutoMapper.IMapper Mapper { get; }

		protected Persistence.IQueryUnitOfWork UnitOfWork { get; }

		protected GiliX.Logging.ILogger<CreateLogCommandHandler> Logger { get; }

		public
			async
			System.Threading.Tasks.Task
			<FluentResults.Result
				<System.Collections.Generic.IEnumerable
				<Persistence.ViewModels.GetLogsQueryResponseViewModel>>>
			Handle(Queries.GetLogsQuery request, System.Threading.CancellationToken cancellationToken)
		{
			var result =
				new FluentResults.Result
				<System.Collections.Generic.IEnumerable
				<Persistence.ViewModels.GetLogsQueryResponseViewModel>>();

			try
			{
				// **************************************************
				var logs =
					await
					UnitOfWork.Logs
					.GetSomeAsync(count: request.Count.Value)
					;
				// **************************************************

				// **************************************************
				result.WithValue(value: logs);
				// **************************************************
			}
			catch (System.Exception ex)
			{
				Logger.LogError
					(exception: ex, message: ex.Message);

				result.WithError
					(errorMessage: ex.Message);
			}

			return result;
		}
	}
}
