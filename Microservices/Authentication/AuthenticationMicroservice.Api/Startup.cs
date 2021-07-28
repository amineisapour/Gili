using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationMicroservice.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Core.DependencyContainer.ConfigureServices(Configuration, services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Core.DependencyContainer.Configure(app, env);
        }
    }
}
