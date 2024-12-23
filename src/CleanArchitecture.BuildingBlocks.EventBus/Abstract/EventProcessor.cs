using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.BuildingBlocks.EventBus.Abstractions
{
    public abstract class EventProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        protected EventProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async Task ProcessEvent(IntegrationEvent @event)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(@event);
            }
        }
    }
}
