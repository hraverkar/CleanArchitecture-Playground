﻿using Autofac;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Email_Notification.Models;
using CleanArchitecture.Application.Logout.Services;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces;
using CleanArchitecture.BuildingBlocks.EventBus;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.AutofacModules
{
    public sealed class InfrastructureModule : Module
    {
        private readonly DbContextOptions<WeatherContext> _options;
        private readonly IConfiguration Configuration;

        public InfrastructureModule(IConfiguration configuration) : this(CreateDbOptions(configuration), configuration)
        {
            configuration.GetSection("MailSettings").Get<MailSettings>();
        }

        public InfrastructureModule(DbContextOptions<WeatherContext> options, IConfiguration configuration)
        {
            Configuration = configuration;
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(Options.Create(DatabaseSettings.Create(Configuration)));
            builder.RegisterType<WeatherContext>()
                .AsSelf()
                .InstancePerRequest()
                .InstancePerLifetimeScope()
                .WithParameter(new NamedParameter("options", _options));
            builder.RegisterType<ServiceBus>().As<IEventBus>().SingleInstance();
            builder.RegisterType<TokenBlackListService>().As<ITokenBlackListService>();

            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));

            builder.RegisterType<NotificationsService>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<EmailNotificationService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static DbContextOptions<WeatherContext> CreateDbOptions(IConfiguration configuration)
        {
            var databaseSettings = DatabaseSettings.Create(configuration);
            var builder = new DbContextOptionsBuilder<WeatherContext>();
#if (UseSqlServer)
            builder.UseSqlServer(databaseSettings.SqlConnectionString);
#else
            builder.UseNpgsql(databaseSettings.PostgresConnectionString);
#endif
            return builder.Options;
        }
    }
}
