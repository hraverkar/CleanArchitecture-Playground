using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.BuildingBlocks.Services.Core.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEnvironmentVariables(this IConfigurationBuilder config, string hostingEnvironment, string basePath = null)
        {
           var builder = new ConfigurationBuilder().AddLocalSettings(basePath);
            
            var config1 = builder.Build();
            basePath = basePath ?? Directory.GetCurrentDirectory();
            config.SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment}.Local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return config;

        }

        public static IConfigurationBuilder AddLocalSettings(this IConfigurationBuilder config, string basePath = null)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = env == Environments.Development;
            var isProduction = env == Environments.Production;
            var isStaging = env == Environments.Staging;
            basePath = basePath ?? Directory.GetCurrentDirectory();
            config.SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.Local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return config;
        }
    }
}
