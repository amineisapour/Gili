using AuthenticationMicroservice.Core.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AuthenticationMicroservice.Core
{
    public static class DependencyContainer
    {
        static DependencyContainer()
        {
        }

        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddApplicationServices(configuration);

            services.AddDbServices(configuration);

            services.AddIdentityServices(configuration);

            services.AddOurSwaager();

            services.AddControllers();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            //app.UseHttpsRedirection();

            app.UseExceptionHandlingMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationMicroservice.Api v1"));
        }
    }
}
