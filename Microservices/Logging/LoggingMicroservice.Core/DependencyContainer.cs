using MediatR;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LoggingMicroservice.Core
{
	public static class DependencyContainer
	{
		static DependencyContainer()
		{
		}

		public static void ConfigureServices(Microsoft.Extensions.Configuration.IConfiguration configuration, Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			// **************************************************
			services.AddTransient
				<Microsoft.AspNetCore.Http.IHttpContextAccessor,
				Microsoft.AspNetCore.Http.HttpContextAccessor>();

			services.AddTransient
				(serviceType: typeof(GiliX.Logging.ILogger<>),
				implementationType: typeof(GiliX.Logging.NLogAdapter<>));
			// **************************************************

			// **************************************************
			// AddMediatR -> Extension Method -> using MediatR;
			// GetTypeInfo -> Extension Method -> using System.Reflection;
			services.AddMediatR
				(typeof(Application.Logs.MappingProfile).GetTypeInfo().Assembly);

			// AddValidatorsFromAssembly -> Extension Method -> using FluentValidation;
			services.AddValidatorsFromAssembly
				(assembly: typeof(Application.Logs.Commands.CreateLogCommandValidator).Assembly);

			services.AddTransient
				(typeof(MediatR.IPipelineBehavior<,>), typeof(GiliX.Mediator.ValidationBehavior<,>));
			// **************************************************

			// **************************************************
			// using Microsoft.Extensions.DependencyInjection;
			services.AddAutoMapper
				(profileAssemblyMarkerTypes: typeof(Application.Logs.MappingProfile));
			// **************************************************

			// **************************************************
			services.AddTransient<Persistence.IUnitOfWork, Persistence.UnitOfWork>(current =>
			{
				string databaseConnectionString =
					configuration
					.GetSection(key: "ConnectionStrings")
					.GetSection(key: "CommandsConnectionString")
					.Value;

				string databaseProviderString =
					configuration
					.GetSection(key: "CommandsDatabaseProvider")
					.Value;

                GiliX.Persistence.Enums.Provider databaseProvider =
					(GiliX.Persistence.Enums.Provider)
					System.Convert.ToInt32(databaseProviderString);

                GiliX.Persistence.Options options =
					new GiliX.Persistence.Options
					{
						Provider = databaseProvider,
						ConnectionString = databaseConnectionString,
					};

				return new Persistence.UnitOfWork(options: options);
			});
			// **************************************************

			// **************************************************
			services.AddTransient<Persistence.IQueryUnitOfWork, Persistence.QueryUnitOfWork>(current =>
			{
				string databaseConnectionString =
					configuration
					.GetSection(key: "ConnectionStrings")
					.GetSection(key: "QueriesConnectionString")
					.Value;

				string databaseProviderString =
					configuration
					.GetSection(key: "QueriesDatabaseProvider")
					.Value;

                GiliX.Persistence.Enums.Provider databaseProvider =
					(GiliX.Persistence.Enums.Provider)
					System.Convert.ToInt32(databaseProviderString);

                GiliX.Persistence.Options options =
					new GiliX.Persistence.Options
					{
						Provider = databaseProvider,
						ConnectionString = databaseConnectionString,
					};

				return new Persistence.QueryUnitOfWork(options: options);
			});
			// **************************************************
		}
	}
}
