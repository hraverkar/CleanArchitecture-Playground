using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Settings
{
    public sealed class DatabaseSettings
    {
        public static DatabaseSettings Create(IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings();
            configuration.GetSection("Database").Bind(databaseSettings);
            configuration.GetSection("ServiceBus").Bind(databaseSettings);
            return databaseSettings;
        }

        public string? ServiceBusConnectionString { get; set; }

#if (UseSqlServer)
        public string? SqlConnectionString { get; set; }
#else
        public string? PostgresConnectionString { get; set; }
#endif
    }
}
