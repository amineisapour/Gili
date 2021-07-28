using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LoggingMicroservice.Api
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        protected Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoggingMicroservice.Api", Version = "v1" });
            });

            // AddFluentValidation->Extension Method-> using FluentValidation.AspNetCore;
            /*
            services.AddControllers()
                .AddFluentValidation(current =>
                {
                    current.RegisterValidatorsFromAssemblyContaining
                        <Application.Logs.Commands.CreateLogCommandValidator>();

                    current.LocalizationEnabled = true; // Default: [true]
                    current.AutomaticValidationEnabled = true; // Default: [true]
                    current.ImplicitlyValidateChildProperties = false; // Default: [false]
                    current.ImplicitlyValidateRootCollectionElements = false; // Default: [false]
                    current.RunDefaultMvcValidationAfterFluentValidationExecutes = false; // Default: [true]
                });
			*/

            Core.DependencyContainer.ConfigureServices(configuration: Configuration, services: services);
        }

        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggingMicroservice.Api v1"));
            }

            //app.UseHttpsRedirection();

            app.UseExceptionHandlingMiddleware();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
