namespace WC.Application
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Contracts;
    using Services;

    /// <summary> Program </summary>
    internal static class Program
    {
        private static IServiceProvider Provider { get; set; }
        private static IConfiguration Configuration { get; set; }

        private static async Task Main()
        {
            try
            {
                Configuration = ReadConfiguration();
                CheckConfiguration();
                Provider = ConfigureServices(new ServiceCollection());

                var calc = Provider.GetRequiredService<Counter>();
                var printer = Provider.GetRequiredService<ReportPrinter>();

                var report = await calc.Process();

                printer.Print(report);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        
        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IReader, TxtReader>();
            services.AddTransient<Counter>();
            services.AddTransient<ReportPrinter>();
            services.Configure<Options>(options => Configuration.GetSection(nameof(Options)).Bind(options));

            return services.BuildServiceProvider();
        }

        private static void CheckConfiguration()
        {
            var options = Configuration.GetSection(nameof(Options)).Get<Options>();
            const string message = "{0} should not be null or empty";

            if (string.IsNullOrEmpty(options.Path))
                throw new Exception(string.Format(message, nameof(options.Path)));

            if (options.Dictionary?.Any() != true)
                throw new Exception(string.Format(message, nameof(options.Dictionary)));

            if (options.Separators?.Any() != true)
                throw new Exception(string.Format(message, nameof(options.Separators)));
        }

        private static IConfiguration ReadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }
    }
}