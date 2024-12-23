using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CleanArchitecture.BuildingBlocks.Services.Core.IntegrationEventHandlers;
using CleanArchitecture.Core.IntegrationEvents;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace CleanArchitecture.BuildingBlocks.Services.App
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.
                RegisterAssemblyTypes(typeof(TaxAutomationEventHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //builder.Register<ServiceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //});
        }
    }
}

