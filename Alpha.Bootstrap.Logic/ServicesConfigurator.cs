using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.Logic
{
    public static class ServicesConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Logic.ServicesConfigurator).Assembly);

            DAL.ServicesConfigurator.ConfigureServices(services);
        }
    }
}
