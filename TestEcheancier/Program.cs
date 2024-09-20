using Microsoft.Extensions.DependencyInjection;
using TestEcheancier.Models;
using TestEcheancier.Services;

namespace TestEcheancier
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            Arguments arguments = new ProgramArgsProcessor().Process(args);
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            serviceProvider.GetService<App>().Run(arguments);

            return 0;
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // required to run the application
            services.AddTransient<App>();
            return services;
        }
    }
}