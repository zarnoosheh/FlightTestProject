using Flight.Services;
using IocLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryLayer;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Flight
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                IConfiguration configuration = BuildConfiguration();

                Log.Logger = ConfigureSerilog(configuration);
                Log.Information("Application Starting");

                IHost host = CreateHost(configuration);
                await RunApplicationAsync(host, args);
            }
            catch (Exception e)
            {
                Log.Fatal(e, "An error occurred while starting the application");
            }
            finally
            {
                Log.CloseAndFlush();
                Console.WriteLine("Press Any Key to close program");
                Console.ReadLine();
            }
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            SetConfigurationSources(builder);
            return builder.Build();
        }

        private static void SetConfigurationSources(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }

        private static ILogger ConfigureSerilog(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static IHost CreateHost(IConfiguration configuration)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context, services, configuration);
                })
                .UseSerilog()
                .Build();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services, IConfiguration configuration)
        {
            services.AddMapping();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddLogging();

            // Register repositories
            services.AddRepositoryInstance();

            // Register service dependencies
            services.AddServicesDependency();
        }

        private static async Task RunApplicationAsync(IHost host, string[] args)
        {
            var svc = ActivatorUtilities.CreateInstance<BasicService>(host.Services);
            await svc.MainMethod(args);
        }
    }
}