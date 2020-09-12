using Alpha.Bootstrap.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Alpha.Bootstrap.WebApi
{
    public class ServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSwagger(services);
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
                    // Localhost: Use this when running from your IDE
                    var connectionString = "Server=127.0.0.1;Port=5432;Database=appDb;Userid=appUser;Password=appPassword;CommandTimeout=30;SSL Mode=Require;Trust Server Certificate=true;";
                    // Docker Local: Use this when running from inside container
                    // var connectionString = "Server=netcore-bootstrap-psql;Port=5432;Database=appDb;Userid=appUser;Password=appPassword;CommandTimeout=30;SSL Mode=Require;Trust Server Certificate=true;";
                    o.UseNpgsql(connectionString);
                });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Alpha.Bootstrap API", Version = "v1" });
                c.EnableAnnotations();
            });
        }
    }
}
