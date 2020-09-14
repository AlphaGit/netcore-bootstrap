using System;
using System.Linq;
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

            // AddHttpClient does not support passing the types as parameters
            // So in order to make it configurable we need to use reflection for the method calls.
            var httpClientRegistrations = ServicesConfigurator.HttpClientRegistrations;
            foreach (var (serviceType, implementationType) in httpClientRegistrations)
            {
                typeof(HttpClientFactoryServiceCollectionExtensions)
                    .GetMethods()
                    .Single(m =>
                        m.Name == nameof(HttpClientFactoryServiceCollectionExtensions.AddHttpClient)
                        && m.GetParameters().Length == 1
                        && m.GetGenericArguments().Length == 2
                    ).MakeGenericMethod(serviceType, implementationType)
                    .Invoke(null, new [] { serviceCollection });
            }


            // TODO: Get this from appSettings.
            var apiBaseUrl = "https://localhost:7326/api/";
            serviceCollection.AddTransient(sp => new Configuration()
            {
                BaseUri = new Uri(apiBaseUrl)
            });
        }
    }
}
