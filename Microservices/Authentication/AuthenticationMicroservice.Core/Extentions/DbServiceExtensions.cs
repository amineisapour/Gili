using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationMicroservice.Core.Extentions
{
    public static class DbServiceExtensions
    {

        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            //});

            string databaseConnectionString =
                configuration
                    .GetSection(key: "ConnectionStrings")
                    .GetSection(key: "QueriesConnectionString")
                    .Value;

            string databaseProviderString =
                configuration
                    .GetSection(key: "QueriesDatabaseProvider")
                    .Value;

            services.AddDbContext<Persistence.DatabaseContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("eShop"));
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(databaseConnectionString);

                //builder.Password = config["DBPassword"];
                options.UseLazyLoadingProxies().UseSqlServer(builder.ConnectionString);
            });

            services.AddTransient<Persistence.IUnitOfWork, Persistence.UnitOfWork>(current =>
            {
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

            services.AddTransient<Persistence.IQueryUnitOfWork, Persistence.QueryUnitOfWork>(current =>
            {
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

            return services;
        }
    }
}
