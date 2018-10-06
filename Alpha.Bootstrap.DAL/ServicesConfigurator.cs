using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.DAL
{
    public static class ServicesConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<BlogDbContext>(o =>
                {
                    o.ConfigureWarnings(w => w.Default(WarningBehavior.Log));
                    //TODO Move into configuration.
                    o.UseNpgsql(
                        "Server=127.0.0.1;Port=5432;Database=appDb;Userid=appUser;Password=appPassword;CommandTimeout=30;");
                });
        }
    }
}
