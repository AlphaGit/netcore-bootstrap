using Alpha.Bootstrap.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.WebApi
{
    public class ServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureFeatures(services);
            ConfigureDataAccess(services);
        }

        protected virtual void ConfigureFeatures(IServiceCollection services)
        {
            services.AddMediatR(typeof(Logic.ServicesConfigurator).Assembly);
        }

        protected virtual void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<BlogDbContext>(o =>
                {
                    o.ConfigureWarnings(w => w.Default(WarningBehavior.Log));

                    // TODO Move into configuration.
                    o.UseNpgsql(
                        "Server=127.0.0.1;Port=5432;Database=appDb;Userid=appUser;Password=appPassword;CommandTimeout=30;");
                });
        }
    }
}
