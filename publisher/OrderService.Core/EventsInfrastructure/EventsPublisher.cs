
using Microsoft.Extensions.DependencyInjection;

public class EventPublisher(IServiceProvider serviceProvider)
{
    public async Task Publish<T>(T @event) where T : IDomainEvent
    {
        var eventHandlers = serviceProvider.GetServices<IEventHandler<T>>();

        foreach (var eventHandler in eventHandlers)
        {
            await eventHandler.Handle(@event);
        }
    }
    
    public async Task PublishDomainEvent(IDomainEvent @event)
    {
        var wrapperType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var eventHandlers = serviceProvider.GetServices(wrapperType);
        foreach (var eventHandler in eventHandlers)
        {
            var invoke = wrapperType.GetMethod("Handle").Invoke(eventHandler, [@event]) as Task;
            await invoke;
        }
    }
}