using System;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.IntegrationTests
{
    public class TestServicesConfigurator : ServicesConfigurator
    {
        protected override void ConfigureDataAccess(IServiceCollection services)
        {
            var appUniqueConnection = Guid.NewGuid();
            services.AddEntityFrameworkSqlite()
                .AddDbContext<BlogDbContext>(options =>
                {
                    options.UseSqlite($"DataSource=file:{appUniqueConnection}?mode=memory&cache=shared");
                    options.ConfigureWarnings(w => w.Log());
                    options.EnableSensitiveDataLogging();
                }, ServiceLifetime.Transient);
        }
    }
}