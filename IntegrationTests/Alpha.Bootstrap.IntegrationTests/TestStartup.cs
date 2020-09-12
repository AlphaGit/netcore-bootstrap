using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        { }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            new TestServicesConfigurator().ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var context = app.ApplicationServices.GetService<BlogDbContext>();
            context.Database.OpenConnection(); // This tells EF that we want to manage the connection manually.
            context.Database.EnsureCreated();
        }
    }
}
