using System;
using System.Threading.Tasks;
using Alpha.Bootstrap.ApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.ConsoleClient
{
    public static class Program
    {
        static async Task Main()
        {
            var serviceProvider = ConfigureServices();

            var application = serviceProvider.GetService<IConsoleClientApplication>();

            await application.Run();
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddTransient<IConsoleClientApplication, ConsoleClientApplication>();

            RegisterApiServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void RegisterApiServices(IServiceCollection serviceCollection)
        {
            var serviceRegistrations = ServicesConfigurator.ServiceRegistrations;

            foreach (var (serviceType, implementationType) in serviceRegistrations)
                serviceCollection.AddTransient(serviceType, implementationType);

            // TODO: Get this from appSettings.
            serviceCollection.AddTransient(sp => new Configuration()
            {
                BaseUri = new Uri("https://localhost:44302/api/")
            });
        }
    }
}
