using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.WebApi
{
    public static class ServicesConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Logic.ServicesConfigurator.ConfigureServices(services);
        }
    }
}
