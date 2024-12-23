using Autofac;
using CleanArchitecture.BuildingBlocks.Services.Core.IntegrationEventHandlers;
using MediatR;


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

        }
    }
}

