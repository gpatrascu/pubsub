using Microsoft.Extensions.DependencyInjection;

public class EventPublisher(IServiceProvider serviceProvider)
{
    public async Task PublishDomainEvent(IDomainEvent @event)
    {
        var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var eventHandlers = serviceProvider.GetServices(eventHandlerType);
        foreach (var eventHandler in eventHandlers)
        {
            var invoke = eventHandlerType.GetMethod("Handle").Invoke(eventHandler, [@event])
                as Task;
            await invoke;
        }
    }
}